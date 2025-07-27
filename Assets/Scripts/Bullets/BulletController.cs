using UnityEngine;

namespace Bullets
{
    public sealed class BulletController
    {
        private readonly IBulletFactory _factory;
        private readonly IBulletCollisionSystem _collisionSystem;
        private readonly IDamageSystem _damageSystem;

        public BulletController(
            IBulletFactory factory,
            IBulletCollisionSystem collisionSystem,
            IDamageSystem damageSystem
        )
        {
            _factory = factory;
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
            if (target.TryGetComponent<IBullet>(out _))
                return;
            
            _collisionSystem.Process(bullet, target, _damageSystem);
            bullet.OnCollision -= HandleHit;
            bullet.OnTriggerEnter -= HandleHit;
            bullet.Stop();
        }
    }
}