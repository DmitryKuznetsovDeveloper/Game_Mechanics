using Core;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemySystem : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private Transform _container;
        [SerializeField] private EnemyView _prefab;
        [SerializeField] private int _initialCount = 16;
        [SerializeField] private int _maxConcurrentEnemies = 10;

        private EnemyPool _pool;
        private EnemyFactory _factory;
        private int _currentEnemyCount;

        private void Awake()
        {
            _factory = ServiceLocator.Resolve<EnemyFactory>();
            ServiceLocator.Resolve<GameCycle>().AddListener(this);  
            
            _pool = new EnemyPool(_prefab, _initialCount, _container);
        }

        public void OnStartGame()
        {
            StartCoroutine(SpawnRoutine());
        }

        public void UnspawnEnemy(GameObject enemy)
        {
            if (!enemy.TryGetComponent(out EnemyView view)) 
                return;
            
            _pool.Unspawn(view);
            _currentEnemyCount = Mathf.Max(0, _currentEnemyCount - 1);
        }

        private System.Collections.IEnumerator SpawnRoutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                
                if (_currentEnemyCount >= _maxConcurrentEnemies)
                    continue;
                
                var view = _pool.Spawn();
                _factory.SetupEnemy(view);
                _currentEnemyCount++; 
            }
        }
    }
}