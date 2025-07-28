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
        [SerializeField] private int _initialCount = 16;
        [SerializeField] private int _maxConcurrentEnemies = 10;

        private EnemyPool _pool;
        private EnemyFactory _factory;
        private int _currentEnemyCount;

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
            {
                _pool.Unspawn(view);
                _currentEnemyCount = Mathf.Max(0, _currentEnemyCount - 1);
            }
        }

        public void InjectDependencies(BulletSystem bulletSystem, GameObject player)
        {
            _factory = new EnemyFactory(_world, player, bulletSystem, this, _positions);
        }

        private System.Collections.IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                
                if (_currentEnemyCount >= _maxConcurrentEnemies)
                    continue;
                
                var view = _pool.Spawn();
                var spawnPos = _positions.RandomSpawnPosition().position;
                var dest = _positions.RandomAttackPosition();
                _factory.SetupEnemy(view, spawnPos, dest);
                _currentEnemyCount++; 
            }
        }
    }
}