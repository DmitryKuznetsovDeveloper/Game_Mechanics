using Components;
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
            if (target.TryGetComponent<HitPointsComponent>(out var hp))
            {
                hp.TakeDamage(amount);
            }
        }
    }
}