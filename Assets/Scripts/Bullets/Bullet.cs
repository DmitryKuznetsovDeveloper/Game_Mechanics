using System;
using GameCycle;
using UnityEngine;
using Zenject;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour, IBullet, IPoolable<BulletData, IMemoryPool>
    {
        public event Action<IBullet, GameObject> OnCollision;
        public event Action<IBullet, GameObject> OnTriggerEnter;

        public int Damage => _damage;
        public bool IsPlayer => _isPlayer;

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private int _damage;
        private bool _isPlayer;
        private Vector2 _direction;
        private MoveComponent _moveComponent;
        private GameManager _gameManager;

        [Inject]
        public void Constructor(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        public void Initialize(BulletData data)
        {
            OnCollision = null;
            OnTriggerEnter = null;
            transform.position = data.Position;
            transform.rotation = data.Rotation;
            _damage = data.Damage;
            _isPlayer = data.IsPlayer;
            _direction = data.Direction;
            gameObject.layer = data.Layer;
            _renderer.color = data.Color;
            _moveComponent = new MoveComponent(data.Speed, _rigidbody2D);
        }

        public void Move()
        {
            _moveComponent.MoveByRigidbodyVelocity(_direction);
        }

        public void Stop()
        {
            _moveComponent.MoveByRigidbodyVelocity(Vector2.zero);
            gameObject.SetActive(false);
        }

        public void OnSpawned(BulletData data, IMemoryPool pool)
        {
            Initialize(data);
            gameObject.SetActive(true);
            _gameManager.AddListener(_moveComponent);
        }

        public void OnDespawned()
        {
            OnCollision = null;
            OnTriggerEnter = null;
            gameObject.SetActive(false);
            _gameManager.RemoveListener(_moveComponent);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollision?.Invoke(this, collision.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter?.Invoke(this, other.gameObject);
        }

        public class Factory : PlaceholderFactory<BulletData, Bullet>
        {
        }

        public class Pool : MonoPoolableMemoryPool<BulletData, IMemoryPool, Bullet>
        {
        }
    }
}