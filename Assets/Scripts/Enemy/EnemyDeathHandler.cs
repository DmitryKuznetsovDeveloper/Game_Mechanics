using Components;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class EnemyDeathHandler : MonoBehaviour
    {
        private HitPointsComponent _hitPointsComponent;
        private EnemySystem _enemySystem;

        private void Awake()
        {
            _hitPointsComponent = GetComponent<HitPointsComponent>();
        }

        private void OnEnable()
        {
            _hitPointsComponent.OnDeath += HandleDeath;
        }

        private void OnDisable()
        {
            _hitPointsComponent.OnDeath -= HandleDeath;
        }

        public void InjectDependencies(EnemySystem system)
        {
            _enemySystem = system;
        }

        private void HandleDeath(GameObject _)
        {
            _hitPointsComponent.ResetHitPoints();
            _enemySystem.UnspawnEnemy(gameObject);
        }
    }
}