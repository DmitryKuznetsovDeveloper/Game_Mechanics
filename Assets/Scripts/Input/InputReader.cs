using System;
using UnityEngine;

namespace Input
{
    public sealed class InputReader : MonoBehaviour
    {
        public event Action OnFirePressed;
        public Vector2 MoveDirection { get; private set; }

        private void Update()
        {
            MoveDirection = Vector2.zero;
            
            var h = UnityEngine.Input.GetAxisRaw("Horizontal");
            var v = UnityEngine.Input.GetAxisRaw("Vertical");
            MoveDirection = new Vector2(h, v).normalized;
            
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                OnFirePressed?.Invoke();
        }
    }
}