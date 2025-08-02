using System.Collections.Generic;
using GameCycle;
using Player;
using Systems;
using UnityEngine;
using Zenject;

namespace Enemys
{
    public class EnemySystem : IGameStartListener, IGameTickable
    {
        private readonly GameManager _gameManager;
        private readonly AttackSystem _attackSystem;
        private readonly EnemyConfig _defaultConfig;
        private readonly PlayerFacade _player;
        private readonly EnemyView.Pool _viewPool;
        private readonly EnemyFacade.Factory _enemyFactory;

        private readonly List<EnemyFacade> _enemies = new();

        public EnemySystem(
            GameManager gameManager, 
            AttackSystem attackSystem, 
            [Inject(Id = "DefaultEnemyConfig")] 
            EnemyConfig defaultConfig,
            PlayerFacade player, 
            EnemyView.Pool viewPool, 
            EnemyFacade.Factory enemyFactory)
        {
            _gameManager = gameManager;
            _attackSystem = attackSystem;
            _defaultConfig = defaultConfig;
            _player = player;
            _viewPool = viewPool;
            _enemyFactory = enemyFactory;
        }
        
        public void OnStartGame()
        {
            for (var i = 0; i < 5; i++)
            {
                Vector2 spawnPos = new Vector2(Random.Range(-5f, 5f), 8f);
                Vector2 attackPos = new Vector2(Random.Range(-5f, 5f), Random.Range(1f, 6f));
                SpawnEnemy(spawnPos, attackPos, _defaultConfig);
            }
        }

        public void Tick(float deltaTime)
        {
            var dt = Time.deltaTime;
            for (var i = _enemies.Count - 1; i >= 0; i--)
            {
                var enemy = _enemies[i];
                enemy.OnFixedUpdateAI(dt);

                if (enemy.HasHitPoints())
                    continue;

                DespawnEnemy(enemy);
                _enemies.RemoveAt(i);
            }
        }

        public void SpawnEnemy(Vector2 spawnPosition, Vector2 attackDestination, EnemyConfig config)
        {
            var view = _viewPool.Spawn();
            view.Transform.position = spawnPosition;

            var enemy = _enemyFactory.Create(_gameManager,_attackSystem, view, config);
            enemy.Initialize(attackDestination, _player);
            _enemies.Add(enemy);
        }

        public void DespawnEnemy(EnemyFacade enemyFacade)
        {
            enemyFacade.Die();
            _viewPool.Despawn(enemyFacade.EnemyView);
        }
    }
}