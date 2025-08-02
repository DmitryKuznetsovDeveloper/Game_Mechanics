using Components;
using UnityEngine;

namespace Player
{
    public sealed class Character
    {
        public GameObject Root => _characterView.Root;
        public Transform Transform => _characterView.Transform;
        public Vector2 Position => _characterView.Position;
        
        private readonly CharacterView _characterView;
        private readonly HitPointsComponent _hitPointsComponent;

        public Character(CharacterView characterView, HitPointsComponent hitPointsComponent)
        {
            _characterView = characterView;
            _hitPointsComponent = hitPointsComponent;
        }
        
        public bool HasHitPoints() => _hitPointsComponent.HasHitPoints();
        
        public void ResetHitPoints() => _hitPointsComponent.ResetHitPoints();
    }
}