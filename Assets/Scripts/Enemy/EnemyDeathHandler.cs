using Components;
using Core;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class EnemyDeathHandler : MonoBehaviour, IGameStartListener, IGameFinishListener
    {
        private HitPointsComponent _hitPointsComponent;
        private EnemySystem _enemySystem;

        private void Awake()
        {
            _enemySystem = ServiceLocator.Resolve<EnemySystem>();
            ServiceLocator.Resolve<GameCycle>().AddListener(this);
            
            _hitPointsComponent = GetComponent<HitPointsComponent>();
        }

        public void OnStartGame()
        {
            _hitPointsComponent.OnDeath += HandleDeath;
        }

        public void OnFinishGame()
        {
            _hitPointsComponent.OnDeath -= HandleDeath;
        }

        private void HandleDeath(GameObject _)
        {
            _hitPointsComponent.ResetHitPoints();
            _enemySystem.UnspawnEnemy(gameObject);
        }
    }
}