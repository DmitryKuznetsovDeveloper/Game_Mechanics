using Systems;
using UnityEngine;
using Zenject;

namespace Enemys
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private EnemyConfig _defaultConfig;
        [SerializeField] private EnemyView _enemyPrefab;
        [SerializeField] private Transform _enemiesContainer;

        public override void InstallBindings()
        {
            Container.BindInstance(_defaultConfig).WithId("DefaultEnemyConfig");

            Container.BindMemoryPool<EnemyView, EnemyView.Pool>()
                .WithInitialSize(16)
                .FromComponentInNewPrefab(_enemyPrefab)
                .UnderTransform(_enemiesContainer);
            Container.Bind<AttackSystem>().AsSingle();
            Container.BindFactory<EnemyView, AttackSystem, EnemyConfig, Transform, Enemy, Enemy.Factory>();
            Container.BindInterfacesAndSelfTo<EnemySystem>().AsSingle();
            
        }
    }
}