using UnityEngine;

namespace Components
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
        
        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
        
        public void MoveByRigidbodyVelocity(Vector2 direction)
        {
            _rb.linearVelocity = direction * _speed;
        }
    }
}