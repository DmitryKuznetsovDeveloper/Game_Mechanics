using UnityEngine;

namespace Core.Shared.Components
{
    public sealed class WeaponComponent
    {
        private readonly Transform _firePoint;

        public WeaponComponent(Transform firePoint)
        {
            _firePoint = firePoint;
        }

        public Vector2 Position => _firePoint.position;
        
        public Transform Transform => _firePoint.transform;

        public Quaternion Rotation => _firePoint.rotation;
    }
}