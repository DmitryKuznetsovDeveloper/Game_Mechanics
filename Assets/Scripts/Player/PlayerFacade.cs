using Components;
using GameCycle;
using UnityEngine;

namespace Player
{
    public sealed class PlayerFacade
    {
        public Vector2 Position => _playerView.Position;
        public AttackComponent AttackComponent => _attackComponent;

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
        private readonly PlayerView _playerView;
        private readonly TeamComponent _teamComponent;
        private readonly MoveComponent _moveComponent;
        private readonly WeaponComponent _weaponComponent;
        private readonly AttackComponent _attackComponent;
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly PlayerDeathHandler _playerDeathHandler;

        public PlayerFacade(
            GameManager gameManager, 
            PlayerView playerView, 
            PlayerConfig playerConfig)
        {
            _gameManager = gameManager;
            _playerView = playerView;
            _playerView.Facade = this;
            
            _teamComponent = new (playerConfig.IsPlayer);
            _moveComponent = new (playerConfig.Speed,playerView.Rigidbody);
            _weaponComponent = new(playerView.FirePoint);
            _attackComponent = new(_teamComponent, _weaponComponent, playerConfig.BulletConfig);
            _hitPointsComponent = new(playerConfig.MaxHitPoints);
            _playerDeathHandler = new(_hitPointsComponent,_gameManager);
            
            _gameManager.AddListener(_moveComponent);
            _gameManager.AddListener(_playerDeathHandler);
        }
        
        public void MoveByRigidbodyVelocityClamped(Vector2 direction, float minX, float maxX) =>
            _moveComponent.MoveByRigidbodyVelocityClamped(direction, minX, maxX);
        
        public bool HasHitPoints() => _hitPointsComponent.HasHitPoints();
        
        public void ResetHitPoints() => _hitPointsComponent.ResetHitPoints();

        public void TakeDamage(int damage)
        {
            _playerView.PlayDamageAnimation();
            _hitPointsComponent.TakeDamage(damage);
        }
    }
}