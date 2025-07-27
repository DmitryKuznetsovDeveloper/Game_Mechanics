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

        public void InjectDependencies(BulletSystem bulletSystem, InputReader inputReader)
        {
            _attackComponent.InjectDependencies(bulletSystem);
            _inputReader = inputReader;
        }
        
        private void Update()
        {
            if (_inputReader.FirePressed)
            {
                _attackComponent.Fire();
            }
        }

        private void FixedUpdate()
        {
            _moveComponent.MoveByRigidbodyVelocity(new Vector2(_inputReader.HorizontalDirection,0));
        }
    }
}