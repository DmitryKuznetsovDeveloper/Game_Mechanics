using Bullets;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(WeaponComponent))]
    public sealed class AttackComponent : MonoBehaviour
    {
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private TeamComponent _teamComponent;
        [SerializeField] private WeaponComponent _weapon;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public void Fire()
        {
            var data = new BulletData
            {
                Position = _weapon.Position,
                Rotation = _weapon.Rotation,
                Speed    = _bulletConfig.Speed,
                Damage   = _bulletConfig.Damage,
                Layer    = (int)_bulletConfig.PhysicsLayer,
                Color    = _bulletConfig.Color,
                IsPlayer = _teamComponent.IsPlayer
            };
            _bulletSystem.Fire(data);
        }
    }
}