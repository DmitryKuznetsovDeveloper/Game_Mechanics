using System.Collections.Generic;
using Player;
using Systems;
using UnityEngine;
using Zenject;

namespace Enemys
{
    public class EnemySystem : IInitializable, ITickable
    {
        private readonly EnemyView.Pool _viewPool;
        private readonly Enemy.Factory _enemyFactory;
        private readonly AttackSystem _attackSystem;
        private readonly EnemyConfig _defaultConfig;
        private readonly Character _player;

        private readonly List<Enemy> _enemies = new();

        public EnemySystem(
            EnemyView.Pool viewPool,
            Enemy.Factory enemyFactory,
            AttackSystem attackSystem,
            [Inject(Id = "DefaultEnemyConfig")] EnemyConfig defaultConfig,
            Character player)
        {
            _viewPool = viewPool;
            _enemyFactory = enemyFactory;
            _attackSystem = attackSystem;
            _defaultConfig = defaultConfig;
            _player = player;
        }

        public void Initialize()
        {
            for (int i = 0; i < 5; i++)
            {
                Vector2 spawnPos = new Vector2(Random.Range(-5f, 5f), 8f);
                Vector2 attackPos = new Vector2(Random.Range(-5f, 5f), Random.Range(1f, 6f));
                SpawnEnemy(spawnPos, attackPos, _defaultConfig);
            }
        }

        public void Tick()
        {
            float dt = Time.deltaTime;
            for (int i = _enemies.Count - 1; i >= 0; i--)
            {
                var enemy = _enemies[i];
                enemy.FixedTick(dt);

                if (!enemy.IsAlive)
                {
                    DespawnEnemy(enemy);
                    _enemies.RemoveAt(i);
                }
            }
        }

        public void SpawnEnemy(Vector2 spawnPosition, Vector2 attackDestination, EnemyConfig config)
        {
            var view = _viewPool.Spawn();
            view.Transform.position = spawnPosition;
            var firePoint = view.FirePoint;

            var enemy = _enemyFactory.Create(
                view,
                _attackSystem,
                config,
                firePoint
            );
            enemy.Initialize(attackDestination, _player);
            _enemies.Add(enemy);
        }

        public void DespawnEnemy(Enemy enemy)
        {
            enemy.Die();
            _viewPool.Despawn(enemy.View);
        }
    }
}
