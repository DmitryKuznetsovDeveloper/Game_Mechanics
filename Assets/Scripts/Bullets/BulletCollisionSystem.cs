using Components;
using UnityEngine;

namespace Bullets
{
    public interface IBulletCollisionSystem
    {
        void Process(IBullet bullet, GameObject target, IDamageSystem damageSystem);
    }

    public sealed class BulletCollisionSystem : IBulletCollisionSystem
    {
        public void Process(IBullet bullet, GameObject target, IDamageSystem damageSystem)
        {
            if (ShouldIgnoreCollision(bullet, target))
                return;

            ApplyDamage(bullet, target, damageSystem);
        }

        private bool ShouldIgnoreCollision(IBullet bullet, GameObject target)
        {
            // Игнорируем любые столкновения пуля ←→ пуля
            if (target.TryGetComponent<IBullet>(out _))
                return true;

            // Игнорируем попадания в «своих»
            return target.TryGetComponent<TeamComponent>(out var team) && team.IsPlayer == bullet.IsPlayer;
        }

        private void ApplyDamage(IBullet bullet, GameObject target, IDamageSystem damageSystem)
        {
            damageSystem.ApplyDamage(target, bullet.Damage, bullet.IsPlayer);
        }
    }
}