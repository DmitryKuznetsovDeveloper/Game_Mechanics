using GameCycle;
using UnityEngine;

namespace Components
{
    public sealed class MoveComponent : IGamePauseListener, IGameResumeListener, IGameFinishListener
    {
        private float _speed;
        private Vector2 _storedVelocity;
        private readonly Rigidbody2D _rb;
        private RigidbodyConstraints2D _originalConstraints;

        public MoveComponent(float speed, Rigidbody2D rb)
        {
            _speed = speed;
            _rb = rb;
            _originalConstraints = _rb.constraints;
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
            Freeze();
        }

        public void Freeze()
        {
            _storedVelocity = _rb.linearVelocity;
            _originalConstraints = _rb.constraints;
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }

        public void Unfreeze()
        {
            _rb.constraints = _originalConstraints;
            _rb.linearVelocity = _storedVelocity;
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