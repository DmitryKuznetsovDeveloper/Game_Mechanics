using System;
using UnityEngine;

namespace Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        public event Action<GameObject> OnDeath;

        [SerializeField] private int _maxHitPoints = 5;
        private int _currentHitPoints;

        private void Awake()
        {
            _currentHitPoints = _maxHitPoints;
        }

        public bool HasHitPoints()
        {
            return _currentHitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            if (_currentHitPoints <= 0)
                return;

            _currentHitPoints = Mathf.Max(0, _currentHitPoints - damage);

            if (_currentHitPoints == 0)
                OnDeath?.Invoke(gameObject);
        }

        public void ResetHitPoints()
        {
            _currentHitPoints = _maxHitPoints;
        }
    }
}