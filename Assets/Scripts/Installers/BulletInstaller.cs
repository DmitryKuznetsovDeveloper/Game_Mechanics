using Bullets;
using Zenject;

namespace Installers
{
    public class BulletInstaller : MonoInstaller
    {
        private const string BULLET_PREFAB_PATH = "Objects/Bullet";

        public override void InstallBindings()
        {
            //Factory and Pool
            Container.BindFactory<BulletData, Bullet, Bullet.Factory>()
                .FromPoolableMemoryPool<BulletData, Bullet, Bullet.Pool>(poolBinder => poolBinder
                    .WithInitialSize(30)
                    .FromComponentInNewPrefabResource(BULLET_PREFAB_PATH)
                    .UnderTransformGroup("Bullets"));
            
            //Main System
            Container.Bind<IBulletCollisionSystem>().To<BulletCollisionSystem>().AsSingle();
            Container.Bind<IDamageSystem>().To<DamageSystem>().AsSingle();
            Container.Bind<BulletController>().AsSingle();
            Container.Bind<BulletSystem>().AsSingle();
        }
    }
}