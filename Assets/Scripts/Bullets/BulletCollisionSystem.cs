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
            // 1) Игнорируем любые столкновения пуля ←→ пуля
            if (target.TryGetComponent<IBullet>(out _))
                return;

            switch (bullet)
            {
                // 2) Игнорируем попадания в «своих»
                case Bullet concreteBullet
                    when target.TryGetComponent<TeamComponent>(out var teamComp)
                         && teamComp.IsPlayer.Equals(concreteBullet.IsPlayer):
                    return;
                // 3) Всем остальным наносим урон
                case Bullet cb:
                    damageSystem.ApplyDamage(target, cb.Damage, cb.IsPlayer);
                    break;
            }
        }

    }
}