using Components;
using Input;
using UnityEngine;

namespace Character
{
    public sealed class PlayerInputController : MonoBehaviour
    {
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private MoveComponent _moveComponent;

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