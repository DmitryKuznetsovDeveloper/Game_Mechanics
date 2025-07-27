using Bullets;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemySystem : MonoBehaviour
    {
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private EnemyPositions _positions;
        [SerializeField] private Transform _world;
        [SerializeField] private Transform _container;
        [SerializeField] private EnemyView _prefab;
        [SerializeField] private int _initialCount = 10;

        private EnemyPool _pool;
        private EnemyFactory _factory;

        private void Awake()
        {
            _pool = new EnemyPool(_prefab, _initialCount, _container);
        }

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }
        
        public void UnspawnEnemy(GameObject enemy)
        {
            if (enemy.TryGetComponent(out EnemyView view))
                _pool.Unspawn(view);
        }
        
        //TODO: пока нет DI 
        public void InjectDependencies(BulletSystem bulletSystem, GameObject player)
        {
            _factory = new EnemyFactory(_world, player, bulletSystem,this);
        }

        private System.Collections.IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);

                var view = _pool.Spawn();
                var spawn = _positions.RandomSpawnPosition().position;
                var dest = _positions.RandomAttackPosition();

                _factory.SetupEnemy(view, spawn, dest);
            }
        }
    }
}