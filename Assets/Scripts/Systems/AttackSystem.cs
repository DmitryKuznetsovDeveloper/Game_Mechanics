using Bullets;
using Components;
using UnityEngine;
using Utils;

namespace Systems
{
    public sealed class AttackSystem
    {
        private readonly AttackComponent _attackComponent;
        private readonly BulletSystem _bulletSystem;

        public AttackSystem(AttackComponent attackComponent, BulletSystem bulletSystem)
        {
            _attackComponent = attackComponent;
            _bulletSystem = bulletSystem;
        }
        
        public void Fire()
        {
            var direction = _attackComponent.WeaponUp;
            FireInDirection(direction);
        }
        
        public void FireAt(Vector2 targetPosition)
        {
            var direction = (_attackComponent.WeaponPosition - targetPosition).normalized;
            FireInDirection(direction);
        }
        
        public void FireInDirection(Vector2 direction)
        {
            var bullet = _attackComponent.MakeBullet(direction);
            _bulletSystem.Fire(bullet);
            DebugUtil.DrawLine(
                _attackComponent.WeaponPosition,
                (Vector3)_attackComponent.WeaponPosition + (Vector3)direction * 10f,
                Color.red, 1f
            );
        }
    }
}