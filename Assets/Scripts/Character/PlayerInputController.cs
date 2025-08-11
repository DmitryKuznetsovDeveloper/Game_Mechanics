using Components;
using Core;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class PlayerInputController : MonoBehaviour, IGameStartListener,IGameFixedUpdateListener,IGameFinishListener
    {
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private MoveComponent _moveComponent;
        
        private InputReader _inputReader;
        
        private void Awake()
        {
            _inputReader = ServiceLocator.Resolve<InputReader>();
            ServiceLocator.Resolve<GameCycle>().AddListener(this);
        }
        
        public void OnStartGame()
        {
            _inputReader.OnFirePressed += HandleFire;
        }

        public void OnFinishGame()
        {
            _inputReader.OnFirePressed -= HandleFire;
        }

        public void OnFixedUpdate(float deltaTime)
        {
            _moveComponent.MoveByRigidbodyVelocity(_inputReader.MoveDirection);
        }

        private void HandleFire()
        {
            _attackComponent.Fire();
        }
    }
}