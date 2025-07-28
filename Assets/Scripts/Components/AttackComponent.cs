using Bullets;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class AttackComponent : MonoBehaviour
    {
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private WeaponComponent _weapon;
        [SerializeField] private BulletConfig _bulletConfig;

        private BulletSystem _bulletSystem;

        public void InjectDependencies(BulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
        }

        public void Fire()
        {
            var bullet = InitBullet(_weapon.transform.up);
            _bulletSystem.Fire(bullet);
        }
        
        public void FireAtTarget(Vector2 targetPosition)
        {
            var direction = (targetPosition - _weapon.Position).normalized;
            var bullet = InitBullet(direction);
            _bulletSystem.Fire(bullet);
            
#if UNITY_EDITOR
            Debug.DrawLine(_weapon.Position, targetPosition, Color.red, 1f);
#endif
        }

        private BulletData InitBullet(Vector2 direction)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0f, 0f, angle);

            var data = new BulletData
            {
                Position = _weapon.Position,
                Rotation = rotation,
                Direction = direction,
                Speed = _bulletConfig.Speed,
                Damage = _bulletConfig.Damage,
                Layer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                IsPlayer = _teamComponent.IsPlayer
            };
            return data;
        }
    }
}