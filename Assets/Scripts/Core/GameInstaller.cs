using Bullets;
using Character;
using Enemy;
using Input;
using ObjectPool;
using UnityEngine;

namespace Core
{
    [DefaultExecutionOrder(-100)]
    public sealed class GameInstaller : MonoBehaviour
    {
        [Header("Main Settings")] [Space]
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private GameCycle _gameCycle;
        [SerializeField] private InputReader _inputReader;

        [Header("Character Settings")] [Space]
        [SerializeField] private CharacterView _playerPrefab;
        [SerializeField] private Transform _spawnPoint;


        [Header("Bullet Settings")] [Space] 
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _poolContainer;
        [SerializeField] private int _initialPoolSize = 20;

        [Header("Enemy Settings")] [Space] 
        [SerializeField] private EnemySystem _enemySystem;

        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private Transform _enemyWorld;

        private void Awake()
        {
            //Main
            ServiceLocator.Register(_gameCycle);
            ServiceLocator.Register(_gameManager);
            ServiceLocator.Register(_inputReader);

            // Character
            var spawnSystem = new CharacterSpawnSystem(_playerPrefab, _spawnPoint);
            ServiceLocator.Register(spawnSystem);

            var character = spawnSystem.SpawnPlayer();
            ServiceLocator.Register(character);

            //Bullets
            ServiceLocator.Register(_bulletSystem);

            var pool = new ObjectPool<Bullet>(_bulletPrefab, _initialPoolSize, _poolContainer);
            ServiceLocator.Register<IPool<Bullet>>(pool);

            var factory = new BulletFactory(pool);
            ServiceLocator.Register<IBulletFactory>(factory);

            ServiceLocator.Register<IBulletCollisionSystem>(new BulletCollisionSystem());

            var damageSystem = new DamageSystem();
            ServiceLocator.Register<IDamageSystem>(damageSystem);

            //Enemy
            var enemyFactory = new EnemyFactory(_enemyWorld);
            ServiceLocator.Register(enemyFactory);
            ServiceLocator.Resolve<GameCycle>().AddListener(enemyFactory);
            
            ServiceLocator.Register(_enemySystem);
            ServiceLocator.Register(_enemyPositions);
            
        }
    }
}