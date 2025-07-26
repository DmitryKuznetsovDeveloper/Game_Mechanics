using System;
using UnityEngine;

namespace ShootEmUp
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnHpEmpty;
        
        [SerializeField] private int _hitPoints;

        public bool HasHitPoints()
        {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints = Mathf.Max(0, _hitPoints - damage);
            
            if (_hitPoints.Equals(0))
            {
                OnHpEmpty?.Invoke(gameObject);
            }
        }
    }
}