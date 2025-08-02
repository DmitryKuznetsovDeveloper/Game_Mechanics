using Components;
using GameCycle;
using Player;
using Systems;
using UnityEngine;
using Zenject;

namespace Enemys
{
    public class Enemy : IGameFixedTickable
    {
        public EnemyView View => _view;
        public GameObject Root => _view.Root;
        public Transform Transform => _view.Transform;
        public Vector2 Position => _view.Position;

        private readonly EnemyView _view;
        private readonly MoveComponent _moveComponent;
        private readonly AttackSystem _attackSystem;
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly WeaponComponent _weaponComponent;
        private readonly AttackComponent _attackComponent;

        private Vector2 _destination;
        private Character _character;
        private float _stopDistance;
        private float _fireCooldown;
        private float _fireTimer;
        private bool _reached;

        public bool IsAlive => _hitPointsComponent.HasHitPoints();

        public Enemy(
            EnemyView view,
            AttackSystem attackSystem,
            EnemyConfig config,
            Transform firePoint)
        {
            _view = view;
            _moveComponent = new MoveComponent(config.MoveSpeed, view.GetComponent<Rigidbody2D>());
            _attackSystem = attackSystem;
            _hitPointsComponent = new HitPointsComponent(config.MaxHitPoints);

            _weaponComponent = new WeaponComponent(firePoint);
            _attackComponent = new AttackComponent(config.TeamComponent, _weaponComponent, config.BulletConfig);

            _stopDistance = config.StopDistance;
            _fireCooldown = config.FireCooldown;
            _fireTimer = _fireCooldown;
        }

        public void Initialize(Vector2 destination, Character character)
        {
            _destination = destination;
            _character = character;
            _reached = false;
            _fireTimer = _fireCooldown;
            _hitPointsComponent.ResetHitPoints();
            _view.Root.SetActive(true);
        }

        public void FixedTick(float deltaTime)
        {
            if (!IsAlive)
                return;

            if (_reached)
                TryAttack(deltaTime);
            else
                TryMove();
        }

        private void TryMove()
        {
            Vector2 current = _view.Transform.position;
            Vector2 delta = _destination - current;

            if (delta.sqrMagnitude < _stopDistance * _stopDistance)
            {
                _reached = true;
                _moveComponent.MoveByRigidbodyVelocity(Vector2.zero);
            }
            else
            {
                _moveComponent.MoveByRigidbodyVelocity(delta.normalized);
            }
        }

        private void TryAttack(float fixedDeltaTime)
        {
            if (!_character.HasHitPoints())
                return;

            _fireTimer -= fixedDeltaTime;
            if (!(_fireTimer <= 0f))
                return;

            _attackSystem.Fire(_attackComponent);
            _fireTimer = _fireCooldown;
        }

        public void Die()
        {
            _view.Root.SetActive(false);
            _moveComponent.MoveByRigidbodyVelocity(Vector2.zero);
        }

        public void Respawn(Vector2 newDestination, Character newTarget)
        {
            Initialize(newDestination, newTarget);
        }

        // Фабрика для врагов через SO-конфиг
        public class Factory : PlaceholderFactory<EnemyView, AttackSystem, EnemyConfig, Transform, Enemy>
        {
            public override Enemy Create(
                EnemyView view,
                AttackSystem attackSystem,
                EnemyConfig config,
                Transform firePoint)
            {
                return new Enemy(view, attackSystem, config, firePoint);
            }
        }
    }
}