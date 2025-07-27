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
            if (bullet is not Bullet concreteBullet)
                return;
            
            if (target.TryGetComponent<TeamComponent>(out var teamComp))
            {
                if (teamComp.IsPlayer == concreteBullet.IsPlayer)
                    return;
            }
            
            damageSystem.ApplyDamage(target, concreteBullet.Damage, concreteBullet.IsPlayer);
        }
    }
}