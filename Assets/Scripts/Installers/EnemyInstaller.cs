using Enemys;
using GameCycle;
using Systems;
using UnityEngine;
using Zenject;

namespace Installers
{
    public sealed class EnemyInstaller
    {
        private readonly EnemyConfig _defaultConfig;
        private readonly Transform _enemyContainer;
        private readonly EnemyPositionsView _enemyPositionsView;
        private readonly int _maxEnemies;

        public EnemyInstaller(EnemyConfig defaultConfig, Transform enemyContainer,
            EnemyPositionsView enemyPositionsView, int maxEnemies)
        {
            _defaultConfig = defaultConfig;
            _enemyContainer = enemyContainer;
            _enemyPositionsView = enemyPositionsView;
            _maxEnemies = maxEnemies;
        }

        private const string ENEMY_PREFAB_PATH = "Objects/Enemy";

        public void InstallBindings(DiContainer container)
        {
            container
                .BindInterfacesAndSelfTo<EnemyPositionService>()
                .AsSingle()
                .WithArguments(_enemyPositionsView.SpawnPoints, _enemyPositionsView.AttackCenter,
                    _enemyPositionsView.AttackRadius, _enemyPositionsView.MinDistanceBetweenPoints);

            container.BindMemoryPool<EnemyView, EnemyView.Pool>()
                .WithInitialSize(16)
                .FromComponentInNewPrefabResource(ENEMY_PREFAB_PATH)
                .UnderTransform(_enemyContainer);

            container.BindFactory<
                    GameManager,
                    AttackSystem,
                    EnemyView,
                    EnemyPositionService,
                    EnemyConfig,
                    EnemyFacade,
                    EnemyFacade.Factory>();  

            container.BindInterfacesAndSelfTo<EnemySystem>().AsSingle();

            container.BindInterfacesAndSelfTo<WaveManager>().AsSingle().WithArguments(_defaultConfig, _maxEnemies);
        }
    }
}