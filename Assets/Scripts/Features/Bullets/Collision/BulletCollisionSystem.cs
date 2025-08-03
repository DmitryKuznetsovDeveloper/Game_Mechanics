using Features.Bullets.View;
using Features.Enemies.View;
using Features.Player.View;
using UnityEngine;

namespace Features.Bullets.Collision
{
    public interface IBulletCollisionSystem
    {
        void Process(IBullet bullet, GameObject target, IDamageSystem damageSystem);
    }

    public sealed class BulletCollisionSystem : IBulletCollisionSystem
    {
        public void Process(IBullet bullet, GameObject target, IDamageSystem damageSystem)
        {
            if (ShouldIgnore(bullet, target))
                return;

            damageSystem.ApplyDamage(target, bullet.Damage, bullet.IsPlayer);
        }

        private bool ShouldIgnore(IBullet bullet, GameObject target)
        {
            if (target.TryGetComponent<BulletView>(out var bv))
            {
                var other = bv.Facade;
                return other.IsPlayer == bullet.IsPlayer;
            }
            
            if (target.TryGetComponent<PlayerView>(out _))
            {
                return bullet.IsPlayer;
            }
            
            if (target.TryGetComponent<EnemyView>(out _))
            {
                return !bullet.IsPlayer;
            }
            
            return true;
        }
    }
}