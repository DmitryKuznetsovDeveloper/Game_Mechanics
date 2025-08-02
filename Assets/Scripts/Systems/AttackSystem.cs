using Bullets;
using Components;
using UnityEngine;
using Utils;

namespace Systems
{
    public sealed class AttackSystem
    {
        private readonly BulletSystem _bulletSystem;

        public AttackSystem(BulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
        }

        public void Fire(AttackComponent attacker)
        {
            var direction = attacker.WeaponUp;
            FireInDirection(attacker, direction);
        }

        public void FireAt(AttackComponent attacker, Vector2 targetPos)
        {
            var direction = (attacker.WeaponPosition - targetPos).normalized;
            FireInDirection(attacker, direction);
        }

        public void FireInDirection(AttackComponent attacker, Vector2 direction)
        {
            var bullet = attacker.MakeBullet(direction);
            _bulletSystem.Fire(bullet);
            DebugUtil.DrawLine(
                attacker.WeaponPosition,
                (Vector3)attacker.WeaponPosition + (Vector3)direction * 10f,
                Color.red, 1f
            );
        }
    }
}