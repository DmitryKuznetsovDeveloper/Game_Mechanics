using UnityEngine;
using ObjectPool;

namespace Enemy
{
    public sealed class EnemyPool
    {
        private readonly IPool<EnemyView> _pool;

        public EnemyPool(EnemyView prefab, int count, Transform container)
        {
            _pool = new ObjectPool<EnemyView>(prefab, count, container);
        }

        public EnemyView Spawn() => _pool.Get();
        public void Unspawn(EnemyView view) => _pool.Return(view);
    }
}