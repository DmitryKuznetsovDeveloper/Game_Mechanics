using UnityEngine;

namespace Input
{
    public sealed class InputReader : MonoBehaviour
    {
        public float HorizontalDirection { get; private set; }
        public bool FirePressed { get; private set; }

        private void Update()
        {
            _ReadHorizontalInput();
            _ReadFireInput();
        }

        private void _ReadHorizontalInput()
        {
            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            {
                HorizontalDirection = -1f;
            }
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
            {
                HorizontalDirection = 1f;
            }
            else
            {
                HorizontalDirection = 0f;
            }
        }

        private void _ReadFireInput()
        {
            FirePressed = UnityEngine.Input.GetKeyDown(KeyCode.Space);
        }
    }
}