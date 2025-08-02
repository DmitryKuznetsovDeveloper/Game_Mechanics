using Bullets;
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
            container.BindFactory<BulletData, Bullet, Bullet.Factory>()
                .FromPoolableMemoryPool<BulletData, Bullet, Bullet.Pool>(poolBinder => poolBinder
                    .WithInitialSize(30)
                    .FromComponentInNewPrefabResource(BULLET_PREFAB_PATH)
                    .UnderTransformGroup("Bullets"));
            
            container.Bind<IBulletCollisionSystem>().To<BulletCollisionSystem>().AsSingle();
            container.Bind<IDamageSystem>().To<DamageSystem>().AsSingle();
            container.Bind<BulletController>().AsSingle();
            container.Bind<BulletSystem>().AsSingle();
        }
    }
}