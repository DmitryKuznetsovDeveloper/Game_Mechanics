using Core.GameCycle;
using Features.Bullets.System;
using Features.Enemies.Configs;
using Features.Enemies.Facade;
using Features.Enemies.System;
using Features.Enemies.View;
using UnityEngine;
using Zenject;

namespace Features.Enemies.Installer
{
    public sealed class EnemyInstaller
    {
        private readonly EnemyConfig _defaultConfig;
        private readonly Transform _enemyContainer;
        private readonly int _maxEnemies;

        public EnemyInstaller(EnemyConfig defaultConfig, Transform enemyContainer, int maxEnemies)
        {
            _defaultConfig = defaultConfig;
            _enemyContainer = enemyContainer;
            _maxEnemies = maxEnemies;
        }

        private const string ENEMY_PREFAB_PATH = "Objects/Enemy";
        private const string ENEMY_POSITION_PREFAB_PATH = "Level/EnemyPositions";

        public void InstallBindings(DiContainer container)
        {
            container
                .BindInterfacesAndSelfTo<EnemyPositionsView>()
                .FromComponentInNewPrefabResource(ENEMY_POSITION_PREFAB_PATH)
                .UnderTransform(_enemyContainer)
                .AsSingle();
            
            container
                .BindInterfacesAndSelfTo<EnemyPositionSystem>()
                .FromNew()
                .AsSingle();

            container.BindMemoryPool<EnemyView, EnemyView.Pool>()
                .WithInitialSize(16)
                .FromComponentInNewPrefabResource(ENEMY_PREFAB_PATH)
                .UnderTransform(_enemyContainer);

            container.BindFactory<
                    GameManager,
                    AttackSystem,
                    EnemyView,
                    EnemyPositionSystem,
                    EnemyConfig,
                    EnemyFacade,
                    EnemyFacade.Factory>();  

            container.BindInterfacesAndSelfTo<EnemySystem>().AsSingle();

            container.BindInterfacesAndSelfTo<WaveSystem>().AsSingle().WithArguments(_defaultConfig, _maxEnemies);
        }
    }
}