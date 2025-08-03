using Core.GameCycle;
using UnityEngine;

namespace Core.Shared.Components
{
    public sealed class MoveComponent : IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        private float _speed;
        private readonly Rigidbody2D _rb;
        private Vector2 _storedVelocity;

        public MoveComponent(float speed, Rigidbody2D rb)
        {
            _speed = speed;
            _rb = rb;
        }

        public void SetSpeed(float speed) => _speed = speed;

        public void MoveByRigidbodyVelocity(Vector2 direction)
        {
            _rb.linearVelocity = direction * _speed;
        }

        public void MoveByRigidbodyVelocityClamped(Vector2 direction, float minX, float maxX)
        {
            var pos = _rb.position;
            var clampedX = Mathf.Clamp(pos.x, minX, maxX);

            direction.y = 0f;

            if (Mathf.Approximately(pos.x, minX) && direction.x < 0f)
                direction.x = 0f;

            if (Mathf.Approximately(pos.x, maxX) && direction.x > 0f)
                direction.x = 0f;

            _rb.linearVelocity = direction * _speed;

            if (!Mathf.Approximately(pos.x, clampedX))
                _rb.position = new Vector2(clampedX, pos.y);
        }

        public void OnPauseGame()
        {
            StopSpeed();
        }

        public void OnResumeGame()
        {
            RevertSpeed();
        }

        public void OnFinishGame()
        {
            StopSpeed();
        }
        
        public void StopSpeed()
        {
            _rb.linearVelocity = Vector2.zero;
            _storedVelocity = _rb.linearVelocity;
        }

        public void RevertSpeed()
        {
            _storedVelocity = _rb.linearVelocity;
        }
    }
}