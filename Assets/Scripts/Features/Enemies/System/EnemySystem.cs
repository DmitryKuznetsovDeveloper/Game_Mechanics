using System.Collections.Generic;
using Core.GameCycle;
using Features.Bullets.System;
using Features.Enemies.Configs;
using Features.Enemies.Facade;
using Features.Enemies.View;
using Features.Player.Facade;
using UnityEngine;

namespace Features.Enemies.System
{
    public sealed class EnemySystem : IGameFixedTickable
    {
        public int CurrentEnemyCount => _enemies.Count;
        
        private readonly GameManager _gameManager;
        private readonly AttackSystem _attackSystem;
        private readonly PlayerFacade _player;
        private readonly EnemyPositionSystem _enemyPositionSystem;
        private readonly EnemyView.Pool _viewPool;
        private readonly EnemyFacade.Factory _enemyFactory;
        private readonly List<EnemyFacade> _enemies = new();

        public EnemySystem(
            GameManager gameManager, 
            AttackSystem attackSystem, 
            PlayerFacade player, 
            EnemyPositionSystem enemyPositionSystem,
            EnemyView.Pool viewPool, 
            EnemyFacade.Factory enemyFactory)
        {
            _gameManager = gameManager;
            _attackSystem = attackSystem;
            _player = player;
            _enemyPositionSystem = enemyPositionSystem;
            _viewPool = viewPool;
            _enemyFactory = enemyFactory;
        }
        
        public void FixedTick(float deltaTime)
        {
            for (var i = _enemies.Count - 1; i >= 0; i--)
            {
                var enemy = _enemies[i];
                enemy.OnFixedUpdateAI(deltaTime);

                if (enemy.HasHitPoints())
                    continue;

                DespawnEnemy(enemy);
                _enemies.RemoveAt(i);
            }
        }

        public void SpawnEnemy(Vector2 spawnPosition, Vector2 attackDestination, EnemyConfig config)
        {
            var view = _viewPool.Spawn();
            view.SetPosition(spawnPosition);

            var enemy = _enemyFactory.Create(_gameManager,_attackSystem, view,_enemyPositionSystem, config);
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