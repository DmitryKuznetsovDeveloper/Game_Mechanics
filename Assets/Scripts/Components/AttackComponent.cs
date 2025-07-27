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

        //TODO: пока нет DI 
        public void InjectDependencies(BulletSystem bulletSystem)
        {
            _bulletSystem = bulletSystem;
        }

        public void Fire()
        {
            Vector2 direction = _weapon.transform.up;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

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

            _bulletSystem.Fire(data);
        }

        public void FireAtTarget(Vector2 targetPosition)
        {
            Vector2 firePosition = _weapon.Position;
            Vector2 direction = (targetPosition - firePosition).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);

            var data = new BulletData
            {
                Position = firePosition,
                Rotation = rotation,
                Direction = direction,
                Speed = _bulletConfig.Speed,
                Damage = _bulletConfig.Damage,
                Layer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                IsPlayer = _teamComponent.IsPlayer
            };

            _bulletSystem.Fire(data);
            
#if UNITY_EDITOR
            Debug.DrawLine(firePosition, targetPosition, Color.red, 1f);
#endif
        }
    }
}