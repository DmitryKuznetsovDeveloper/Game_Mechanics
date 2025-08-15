using Features.Enemies.View;
using Features.Player.View;
using UnityEngine;

namespace Features.Bullets.Collision
{
    public interface IDamageSystem
    {
        void ApplyDamage(GameObject target, int amount, bool isPlayer);
    }

    public sealed class DamageSystem : IDamageSystem
    {
        public void ApplyDamage(GameObject target, int amount, bool isPlayer)
        {
            
            if (target.TryGetComponent<EnemyView>(out var ev))
            {
                ev.Facade.TakeDamage(amount);
                return;
            }

            if (!target.TryGetComponent<PlayerView>(out var pv)) 
                return;
            
            pv.Facade.TakeDamage(amount);
        }
    }
}