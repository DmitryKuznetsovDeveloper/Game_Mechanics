using Core.GameCycle;
using Core.Shared.Components;
using Features.Bullets.System;
using Features.Enemies.Configs;
using Features.Enemies.System;
using Features.Enemies.View;
using Features.Player.Facade;
using UnityEngine;
using Zenject;

namespace Features.Enemies.Facade
{
    public sealed class EnemyFacade
    {
        public EnemyView EnemyView => _enemyView;

        public bool IsPlayer
        {
            get => _teamComponent.IsPlayer;
            set
            {
                if (_teamComponent.IsPlayer.Equals(value))
                    return;

                _teamComponent.IsPlayer = value;
            }
        }

        private readonly GameManager _gameManager;
        private readonly AttackSystem _attackSystem;
        private readonly EnemyView _enemyView;
        private readonly EnemyPositionSystem _enemyPositionSystem;
        private readonly EnemyConfig _enemyConfig;
        private readonly TeamComponent _teamComponent;
        private readonly MoveComponent _moveComponent;
        private readonly WeaponComponent _weaponComponent;
        private readonly AttackComponent _attackComponent;
        private readonly HitPointsComponent _hitPointsComponent;

        private PlayerFacade _playerFacade;
        private Vector2 _destination;
        private float _fireTimer;
        private bool _reached;

        public EnemyFacade(
            GameManager gameManager,
            AttackSystem attackSystem,
            EnemyView enemyView,
            EnemyPositionSystem enemyPositionSystem,
            EnemyConfig enemyConfig)
        {
            _gameManager = gameManager;
            _attackSystem = attackSystem;
            _enemyView = enemyView;
            _enemyView.Facade = this;
            _enemyPositionSystem = enemyPositionSystem;
            _enemyConfig = enemyConfig;
            _teamComponent = new(enemyConfig.IsPlayer);
            _moveComponent = new(enemyConfig.Speed, enemyView.Rigidbody);
            _weaponComponent = new(_enemyView.FirePoint);
            _attackComponent = new(_teamComponent, _weaponComponent, enemyConfig.BulletConfig);
            _hitPointsComponent = new(enemyConfig.MaxHitPoints);

            _gameManager.AddListener(_moveComponent);
        }

        public void Initialize(Vector2 destination, PlayerFacade playerFacade)
        {
            _destination = destination;
            _playerFacade = playerFacade;
            _reached = false;
            ResetHitPoints();
            _enemyView.Root.SetActive(true);
        }

        public void OnFixedUpdateAI(float deltaTime)
        {
            if (!HasHitPoints())
                return;

            if (_reached)
                TryAttack(deltaTime);
            else
                TryMove();
        }

        private void TryMove()
        {
            Vector2 current = _enemyView.Position;
            Vector2 delta = _destination - current;

            if (delta.sqrMagnitude < _enemyConfig.StopDistance * _enemyConfig.StopDistance)
            {
                _reached = true;
                _moveComponent.StopSpeed();
            }
            else
            {
                _moveComponent.MoveByRigidbodyVelocity(delta);
            }
        }

        private void TryAttack(float fixedDeltaTime)
        {
            if (!_playerFacade.HasHitPoints())
                return;

            _fireTimer -= fixedDeltaTime;
            if (!(_fireTimer <= 0f))
                return;

            _attackSystem.FireAt(_attackComponent, _playerFacade.Position);
            _fireTimer = _enemyConfig.FireCooldown;

            _destination = PickNewDestination(_destination);
            _reached = false;
        }
        
        public Vector2 PickNewDestination(Vector2 currentPoint)
        {
            _enemyPositionSystem.ReleaseAttackPoint(currentPoint);
            return _enemyPositionSystem.RandomAttackPoint();
        }

        public bool HasHitPoints() => _hitPointsComponent.HasHitPoints();

        public void ResetHitPoints() => _hitPointsComponent.ResetHitPoints();

        public void TakeDamage(int damage)
        {
            _enemyView.PlayDamageAnimation();
            _hitPointsComponent.TakeDamage(damage);
        }

        public void Die()
        {
            _enemyView.Root.SetActive(false);
            _moveComponent.MoveByRigidbodyVelocity(Vector2.zero);
        }

        public class
            Factory : PlaceholderFactory<GameManager, AttackSystem, EnemyView,
            EnemyPositionSystem, EnemyConfig, EnemyFacade>
        {
            public override EnemyFacade Create(
                GameManager gameManager,
                AttackSystem attackSystem,
                EnemyView view,
                EnemyPositionSystem enemyPositionSystem,
                EnemyConfig config)
            {
                return new EnemyFacade(gameManager, attackSystem, view, enemyPositionSystem, config);
            }
        }
    }
}