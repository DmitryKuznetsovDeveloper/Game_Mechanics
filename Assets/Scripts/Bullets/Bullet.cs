using System;
using Components;
using UnityEngine;

namespace Bullets
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(SpriteRenderer))]
    public sealed class Bullet : MonoBehaviour, IBullet
    {
        public event Action<IBullet, GameObject> OnCollision;
        public event Action<IBullet, GameObject> OnTriggerEnter;
        
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private MoveComponent _moveComponent;
    
        private int _damage;
        private bool _isPlayer;
        private float _speed;
        private Vector2 _direction;

        public void Initialize(BulletData data)
        {
            transform.position = data.Position;
            transform.rotation = data.Rotation;
            _damage = data.Damage;
            _isPlayer = data.IsPlayer;
            _direction = data.Direction;
    
            gameObject.layer = data.Layer;
            _renderer.color = data.Color;
            _moveComponent.SetSpeed(data.Speed);
        }
    
        public void Fire()
        {
            _moveComponent.MoveByRigidbodyVelocity(_direction);
        }
    
        public void Stop()
        {
            _moveComponent.MoveByRigidbodyVelocity(Vector2.zero);
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollision?.Invoke(this, collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter?.Invoke(this, other.gameObject);
        }

        public int Damage    => _damage;
        public bool IsPlayer => _isPlayer;
    }
}