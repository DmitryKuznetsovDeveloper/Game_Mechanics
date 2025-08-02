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
        private readonly Transform _enemySpawnPoint;

        public EnemyInstaller(EnemyConfig defaultConfig, Transform enemySpawnPoint)
        {
            _defaultConfig = defaultConfig;
            _enemySpawnPoint = enemySpawnPoint;
        }

        private const string ENEMY_PREFAB_PATH = "Objects/Enemy";

        public void InstallBindings(DiContainer container)
        {
            container.BindInstance(_defaultConfig).WithId("DefaultEnemyConfig");

            container.BindMemoryPool<EnemyView, EnemyView.Pool>()
                .WithInitialSize(16)
                .FromComponentInNewPrefabResource(ENEMY_PREFAB_PATH)
                .UnderTransform(_enemySpawnPoint);

            container.Bind<AttackSystem>().AsSingle();

            container.BindFactory<GameManager, AttackSystem, EnemyView, 
                EnemyConfig, EnemyFacade, EnemyFacade.Factory>();
            
            container.BindInterfacesAndSelfTo<EnemySystem>().AsSingle();
        }
    }
}