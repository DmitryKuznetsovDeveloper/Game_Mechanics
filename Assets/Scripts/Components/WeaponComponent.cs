using UnityEngine;

namespace Components
{
    public class WeaponComponent : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        
        public Vector2 Position => !_firePoint ? transform.position : _firePoint.position;

        public Quaternion Rotation => !_firePoint ? transform.rotation : _firePoint.rotation;
    }
}