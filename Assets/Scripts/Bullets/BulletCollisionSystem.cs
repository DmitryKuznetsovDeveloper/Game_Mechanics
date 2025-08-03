using Enemys;
using Player;
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
            if (ShouldIgnore(bullet, target))
                return;

            damageSystem.ApplyDamage(target, bullet.Damage, bullet.IsPlayer);
        }

        private bool ShouldIgnore(IBullet bullet, GameObject target)
        {
            // 1) пуля ←→ пуля
            if (target.TryGetComponent<BulletView>(out var bv))
            {
                var other = bv.Facade;
                // игнорируем, если это «своя» пуля
                return other.IsPlayer == bullet.IsPlayer;
            }

            // 2) пуля игрока ←→ игрок
            if (target.TryGetComponent<PlayerView>(out _))
            {
                // если это пуля игрока и цель — игрок, игнорируем
                return bullet.IsPlayer;
            }

            // 3) пуля врага ←→ враг
            if (target.TryGetComponent<EnemyView>(out _))
            {
                // если это пуля врага и цель — враг, игнорируем
                return !bullet.IsPlayer;
            }

            // 4) всё прочее (стены и пр.) — игнорируем
            return true;
        }
    }
}