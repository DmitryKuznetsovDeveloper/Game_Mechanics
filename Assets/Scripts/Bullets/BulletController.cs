using UnityEngine;

namespace Bullets
{
    public class BulletController
    {
        private readonly Bullet.Factory _factory;
        private readonly IBulletCollisionSystem _collisionSystem;
        private readonly IDamageSystem _damageSystem;
        
        public BulletController(
            Bullet.Factory factory,
            IBulletCollisionSystem collisionSystem,
            IDamageSystem damageSystem)
        {
            _factory = factory;
            _collisionSystem = collisionSystem;
            _damageSystem = damageSystem;
        }
        
        public void Fire(BulletData data)
        {
            var bullet = _factory.Create(data);
            bullet.OnCollision += HandleHit;
            bullet.Move();
        }
        
        private void HandleHit(IBullet bullet, GameObject target)
        {
            _collisionSystem.Process(bullet, target, _damageSystem);
            bullet.OnCollision -= HandleHit;
            if (bullet is Bullet b)
                b.Stop();
        }
    }
}