using System;
using GameCycle;
using UnityEngine;

namespace Input
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
            var v = UnityEngine.Input.GetAxisRaw("Vertical");
            var direction = new Vector2(h, v);
            return direction.sqrMagnitude > 0 ? direction.normalized : Vector2.zero;
        }

        private void CheckFireInput()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                OnFirePressed?.Invoke();
        }
    }
}