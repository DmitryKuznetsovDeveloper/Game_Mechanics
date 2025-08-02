using Bullets;
using UnityEngine;

namespace Components
{
    public sealed class AttackComponent
    {
        public Vector2 WeaponUp => _weapon.Transform.up;
        public Vector2 WeaponRight => _weapon.Transform.right;
        public Vector2 WeaponPosition => _weapon.Position;
        
        private readonly TeamComponent _teamComponent;
        private readonly WeaponComponent _weapon;
        private readonly BulletConfig _bulletConfig;

        public AttackComponent(TeamComponent teamComponent, WeaponComponent weapon, BulletConfig bulletConfig)
        {
            _teamComponent = teamComponent;
            _weapon = weapon;
            _bulletConfig = bulletConfig;
        }

        public BulletData MakeBullet(Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0f, 0f, angle);

            return new BulletData
            {
                Position = _weapon.Position,
                Rotation = rotation,
                Direction = direction,
                Speed = _bulletConfig.Speed,
                LifeTime = _bulletConfig.Lifetime,
                Damage = _bulletConfig.Damage,
                Layer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                IsPlayer = _teamComponent.IsPlayer
            };
        }
    }
}