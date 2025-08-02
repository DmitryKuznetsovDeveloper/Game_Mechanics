using System;
using System.Threading;
using Components;
using Cysharp.Threading.Tasks;
using GameCycle;
using UnityEngine;
using Utils;
using Zenject;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour, IBullet, IPoolable<BulletData, IMemoryPool>
    {
        public event Action<IBullet, GameObject> OnCollision;

        public int Damage => _damage;
        public bool IsPlayer => _isPlayer;

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private int _lifeTime;
        private int _damage;
        private bool _isPlayer;
        private Vector2 _direction;
        private MoveComponent _moveComponent;
        private GameManager _gameManager;
        private TimerComponent _timer;
        private CancellationTokenSource _lifetimeCts;
        private IMemoryPool _pool;
        [Inject]
        public void Constructor(GameManager gameManager, TimerComponent timer)
        {
            _gameManager = gameManager;
            _timer = timer;
        }

        public void Initialize(BulletData data)
        {
            OnCollision = null;
            transform.position = data.Position;
            transform.rotation = data.Rotation;
            _lifeTime = data.LifeTime;
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
            _lifetimeCts?.Cancel();
            _pool?.Despawn(this);
        }

        public void OnSpawned(BulletData data, IMemoryPool pool)
        {
            Initialize(data);
            gameObject.SetActive(true);
            _gameManager.AddListener(_moveComponent);

            _pool = pool;

            _lifetimeCts = new CancellationTokenSource();
            DespawnWithTimerAsync(_lifeTime, _lifetimeCts.Token).Forget();
        }

        public void OnDespawned()
        {
            OnCollision = null;
            gameObject.SetActive(false);
            _gameManager.RemoveListener(_moveComponent);

            _lifetimeCts?.Cancel(); 
            _lifetimeCts?.Dispose();
            _lifetimeCts = null;

            _pool = null;
        }
        
        private async UniTaskVoid DespawnWithTimerAsync(int lifetime, CancellationToken token)
        {
            try
            {
                await _timer.CountdownAsync(lifetime, null, token);
                if (!token.IsCancellationRequested)
                    Stop();
            }
            catch (OperationCanceledException)
            {
                DebugUtil.Log("отмена таймера пули");
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollision?.Invoke(this, collision.gameObject);
        }
        
        public class Factory : PlaceholderFactory<BulletData, Bullet>
        {
        }

        public class Pool : MonoPoolableMemoryPool<BulletData, IMemoryPool, Bullet>
        {
        }
    }
}