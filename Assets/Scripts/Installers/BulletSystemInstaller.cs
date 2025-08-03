using Bullets;
using Systems;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class BulletSystemInstaller
    {
        private readonly Transform _spawnPoint;

        public BulletSystemInstaller(Transform spawnPoint)
        {
            _spawnPoint = spawnPoint;
        }

        private const string BULLET_PREFAB_PATH = "Objects/Bullet";

        public void InstallBindings(DiContainer container)
        {
            container.BindMemoryPool<BulletView, BulletView.Pool>()
                    .WithInitialSize(30)
                    .FromComponentInNewPrefabResource(BULLET_PREFAB_PATH)
                    .UnderTransform(_spawnPoint);
            
            container.BindFactory<BulletView, BulletFacade, BulletFacade.Factory>();
            
            container.Bind<IBulletCollisionSystem>().To<BulletCollisionSystem>().AsSingle();
            container.Bind<IDamageSystem>().To<DamageSystem>().AsSingle();
            container.BindInterfacesAndSelfTo<BulletController>().AsSingle();
            container.Bind<AttackSystem>().AsSingle();
            container.Bind<BulletSystem>().AsSingle();
        }
    }
}