using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private float _speed = 5.0f;
        private Rigidbody2D _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void MoveByRigidbodyVelocity(Vector2 direction)
        {
            var nextPosition = _rb.position + direction * _speed;
            _rb.MovePosition(nextPosition);
        }
    }
}