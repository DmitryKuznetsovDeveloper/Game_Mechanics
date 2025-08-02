using Components;
using GameCycle;
using Player;
using Systems;
using UnityEngine;
using Zenject;

namespace Enemys
{
    public class EnemyFacade
    {
        public EnemyView EnemyView => _enemyView;
        public GameObject Root => _enemyView.Root;
        public Transform Transform => _enemyView.Transform;
        public Vector2 Position => _enemyView.Position;
        public Rigidbody2D Rigidbody => _enemyView.Rigidbody;
        public Transform FirePoint => _enemyView.FirePoint;

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
            EnemyConfig config)
        {
            _gameManager = gameManager;
            _attackSystem = attackSystem;
            _enemyView = enemyView;
            _teamComponent = new(config.IsPlayer);
            _moveComponent = new(config.Speed, enemyView.Rigidbody);
            _weaponComponent = new(_enemyView.FirePoint);
            _attackComponent = new(_teamComponent, _weaponComponent, config.BulletConfig);
            _hitPointsComponent = new(config.MaxHitPoints);

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
                Freeze();
            }
            else
            {
                Unfreeze();
            }
        }

        private void TryAttack(float fixedDeltaTime)
        {
            if (!_playerFacade.HasHitPoints())
                return;

            _fireTimer -= fixedDeltaTime;
            if (!(_fireTimer <= 0f))
                return;

            _attackSystem.Fire(_attackComponent);
            _fireTimer = _enemyConfig.FireCooldown;
        }

        public void SetSpeed(float speed) => _moveComponent.SetSpeed(speed);

        public void Freeze() => _moveComponent.Freeze();

        public void Unfreeze() => _moveComponent.Unfreeze();

        public void Respawn(Vector2 newDestination, PlayerFacade newTarget)
        {
            Initialize(newDestination, newTarget);
        }

        public bool HasHitPoints() => _hitPointsComponent.HasHitPoints();

        public void ResetHitPoints() => _hitPointsComponent.ResetHitPoints();

        public void TakeDamage(int damage) => _hitPointsComponent.TakeDamage(damage);

        public void Die()
        {
            _enemyView.Root.SetActive(false);
            _moveComponent.MoveByRigidbodyVelocity(Vector2.zero);
        }
        
        public class Factory : PlaceholderFactory<GameManager, AttackSystem, EnemyView, EnemyConfig, EnemyFacade>
        {
            public override EnemyFacade Create(
                GameManager gameManager,
                AttackSystem attackSystem,
                EnemyView view,
                EnemyConfig config)
            {
                return new EnemyFacade(gameManager, attackSystem, view, config);
            }
        }
    }
}