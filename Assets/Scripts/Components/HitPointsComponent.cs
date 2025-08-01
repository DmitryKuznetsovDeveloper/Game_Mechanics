using System;

namespace Components
{
    public sealed class HitPointsComponent
    {
        public event Action OnDeath;

        private int _currentHitPoints;
        private readonly int _maxHitPoints;

        public HitPointsComponent(int maxHitPoints)
        {
            _maxHitPoints = maxHitPoints;
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

            _currentHitPoints = Math.Max(0, _currentHitPoints - damage);

            if (_currentHitPoints == 0)
                OnDeath?.Invoke();
        }

        public void ResetHitPoints()
        {
            _currentHitPoints = _maxHitPoints;
        }
    }
}