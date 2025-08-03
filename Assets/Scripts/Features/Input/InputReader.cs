using System;
using Core.GameCycle;
using UnityEngine;

namespace Features.Input
{
    public sealed class InputReader : IGameTickable
    {
        public event Action OnFirePressed;
        public Vector2 MoveDirection { get; private set; }
        
        public void Tick(float deltaTime)
        {
            UpdateMoveDirection();
            CheckFireInput();
        }

        private void UpdateMoveDirection()
        {
            var direction = GetInputDirection();
            MoveDirection = direction;
        }

        private Vector2 GetInputDirection()
        {
            var h = UnityEngine.Input.GetAxisRaw("Horizontal");
            var direction = new Vector2(h, 0);
            return direction.sqrMagnitude > 0 ? direction.normalized : Vector2.zero;
        }

        private void CheckFireInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                OnFirePressed?.Invoke();
        }
    }
}