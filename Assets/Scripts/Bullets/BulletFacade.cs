using System;
using Components;
using GameCycle;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletFacade : IBullet
    {
        public event Action<IBullet, GameObject> OnCollision;

        public int Damage { get; private set; }
        public bool IsPlayer { get; private set; }

        public BulletView View => _view;

        private readonly GameManager _gameManager;
        private readonly BulletView _view;

        private MoveComponent _moveComponent;
        private float _lifetime;
        private Vector2 _direction;
        private bool _moving;

        public BulletFacade(GameManager gameManager, BulletView view)
        {
            view.Facade = this;
            _gameManager = gameManager;
            _view = view;
            _view.OnCollisionEnter += HandleCollision;
            _moveComponent = new(0, view.Rigidbody);
            _moving = false;
            _gameManager.AddListener(_moveComponent);
        }

        public void Initialize(BulletData data)
        {
            _direction = data.Direction;
            _lifetime = data.Config.Lifetime;
            Damage = data.Config.Damage;
            IsPlayer = data.IsPlayer;
            _moveComponent = new(data.Config.Speed, _view.Rigidbody);
            _view.SetPosition(data.Position);
            _view.SetRotation(data.Rotation);
            _view.SetColor(data.Config.Color);
            _view.SetLayer(data.Config.PhysicsLayer);
            _moving = false;
            
        }

        public void Move()
        {
            _moving = true;
        }

        public void Stop()
        {
            _moving = false;
            View.gameObject.SetActive(false);
            _moveComponent.MoveByRigidbodyVelocity(Vector2.zero);
        }

        public void OnFixedTick(float deltaTime)
        {
            if (!_moving)
                return;

            _moveComponent.MoveByRigidbodyVelocity(_direction);

            _lifetime -= deltaTime;
            if (_lifetime <= 0f)
                Stop();
        }

        private void HandleCollision(Collision2D coll)
        {
            OnCollision?.Invoke(this, coll.gameObject);
        }

        public class Factory : Zenject.PlaceholderFactory<BulletView, BulletFacade>
        {
        }
    }
}