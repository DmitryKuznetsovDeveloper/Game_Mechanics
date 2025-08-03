using System.Collections.Generic;
using Core.GameCycle;
using Features.Bullets.Collision;
using Features.Bullets.Facade;
using Features.Bullets.View;
using UnityEngine;

namespace Features.Bullets.System
{
    public sealed class BulletController : IGameFixedTickable
    {
        private readonly BulletView.Pool _bulletViewPool;
        private readonly BulletFacade.Factory _facadeFactory;
        private readonly IBulletCollisionSystem _collisionSystem;
        private readonly IDamageSystem _damageSystem;
        private readonly List<BulletFacade> _bullets = new();

        public BulletController(
            BulletView.Pool pool, 
            IBulletCollisionSystem collisionSystem, 
            IDamageSystem damageSystem,
            BulletFacade.Factory factory)
        {
            _bulletViewPool = pool;
            _facadeFactory = factory;
            _collisionSystem = collisionSystem;
            _damageSystem = damageSystem;
        }

        public void Fire(BulletData data)
        {
            var view = _bulletViewPool.Spawn();
            var bullet = _facadeFactory.Create(view);
            bullet.Initialize(data);
            bullet.OnCollision += HandleHit;
            bullet.Move();
            _bullets.Add(bullet);
        }

        public void FixedTick(float deltaTime)
        {
            for (int i = _bullets.Count - 1; i >= 0; i--)
            {
                var bullet = _bullets[i];
                bullet.OnFixedTick(deltaTime);
            
                if (bullet.View.gameObject.activeSelf) 
                    continue;
            
                _bullets.RemoveAt(i);
                _bulletViewPool.Despawn(bullet.View);
            }
        }

        private void HandleHit(IBullet bullet, GameObject target)
        {
            _collisionSystem.Process(bullet, target, _damageSystem);
            bullet.Stop();
        }
    }
}