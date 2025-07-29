using Core;
using ObjectPool;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour
    {
        private BulletController _bulletController;
        
        private void Awake()
        {
            _bulletController = new BulletController(
                ServiceLocator.Resolve<IBulletFactory>(),
                ServiceLocator.Resolve<IPool<Bullet>>(),
                ServiceLocator.Resolve<IBulletCollisionSystem>(),
                ServiceLocator.Resolve<IDamageSystem>()
            );
        }
        
        public void Fire(BulletData data)
        {
            _bulletController.Fire(data);
        }
    }
}