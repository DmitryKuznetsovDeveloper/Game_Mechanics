using Player;
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
            
            
            var prefab = Resources.Load<GameObject>("Objects/Character");
            // 2. Спавним его на сцену в нужной позиции
            var go = GameObject.Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);

            // 3. Если нужно, можем получить GameObjectContext
            var context = go.GetComponent<Zenject.GameObjectContext>();
            var character = context.Container.Resolve<PlayerFacade>();
            Container.BindInterfacesAndSelfTo<EnemySystem>().AsSingle().WithArguments(character);
        }
    }
}