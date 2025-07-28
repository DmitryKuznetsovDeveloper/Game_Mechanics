using Bullets;
using Components;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private MoveComponent _moveComponent;
        
        private InputReader _inputReader;
        
        private void Start()
        {
            _inputReader.OnFirePressed += HandleFire;
        }

        private void OnDisable()
        {
            _inputReader.OnFirePressed -= HandleFire;
        }

        private void FixedUpdate()
        {
            _moveComponent.MoveByRigidbodyVelocity(_inputReader.MoveDirection);
        }

        private void HandleFire()
        {
            _attackComponent.Fire();
        }
        
        public void InjectDependencies(BulletSystem bulletSystem, InputReader inputReader)
        {
            _attackComponent.InjectDependencies(bulletSystem);
            _inputReader = inputReader;
        }
    }
}