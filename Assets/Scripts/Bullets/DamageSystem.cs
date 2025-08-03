using Components;
using Enemys;
using Player;
using UnityEngine;

namespace Bullets
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