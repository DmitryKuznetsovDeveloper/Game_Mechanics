using UnityEngine;
using ObjectPool;

namespace Bullets
{
    public sealed class BulletController
    {
        private readonly IBulletFactory _factory;
        private readonly IPool<Bullet> _pool;
        private readonly IBulletCollisionSystem _collisionSystem;
        private readonly IDamageSystem _damageSystem;

        public BulletController(
            IBulletFactory factory,
            IPool<Bullet> pool,
            IBulletCollisionSystem collisionSystem,
            IDamageSystem damageSystem
        )
        {
            _factory = factory;
            _pool = pool;
            _collisionSystem = collisionSystem;
            _damageSystem = damageSystem;
        }

        public void Fire(BulletData data)
        {
            var bullet = _factory.Create(data);
            bullet.OnCollision += HandleHit;
            bullet.OnTriggerEnter += HandleHit;
            bullet.Fire();
        }

        private void HandleHit(IBullet bullet, GameObject target)
        {
            // Игнорируем попадание в другие пули
            if (target.TryGetComponent<IBullet>(out _))
                return;

            _collisionSystem.Process(bullet, target, _damageSystem);
            bullet.OnCollision -= HandleHit;
            bullet.OnTriggerEnter -= HandleHit;
            _pool.Return((Bullet)bullet);
        }
    }
}