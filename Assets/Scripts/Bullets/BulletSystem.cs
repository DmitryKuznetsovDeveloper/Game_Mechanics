using ObjectPool;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour
    {
        [Header("Pool Settings")]
        [SerializeField] private Transform _poolContainer;
        [SerializeField] private int _initialPoolSize = 20;
        [SerializeField] private Bullet _bulletPrefab;

        private BulletController _bulletController;

        private void Awake()
        {
            var pool = new ObjectPool<Bullet>(_bulletPrefab, _initialPoolSize, _poolContainer);
            var factory = new BulletFactory(pool);
            var collisionSystem = new BulletCollisionSystem();
            var damageSystem = new DamageSystem();
            _bulletController = new BulletController(factory, collisionSystem, damageSystem);
        }
        
        public void Fire(BulletData data)
        {
            _bulletController.Fire(data);
        }
    }
}