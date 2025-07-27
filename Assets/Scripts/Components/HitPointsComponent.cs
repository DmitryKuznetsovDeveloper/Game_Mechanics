using System;
using UnityEngine;

namespace Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnDeath;
        
        [SerializeField] private int _hitPoints;

        public bool HasHitPoints()
        {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints = Mathf.Max(0, _hitPoints - damage);
            
            if (_hitPoints.Equals(0)) 
                OnDeath?.Invoke(gameObject);
        }
    }
}