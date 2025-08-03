using Core.GameCycle;
using Features.Level.View;
using UnityEngine;

namespace Features.Level.System
{
    public sealed class BackgroundScroller : IGameTickable
    {
        private readonly BackgroundScrollerView _view;
        private readonly float _scrollSpeedY;
        private Vector2 _offset;

        public BackgroundScroller(BackgroundScrollerView view, float scrollSpeedY)
        {
            _view = view;
            _scrollSpeedY = scrollSpeedY;
        }
        
        public void Tick(float deltaTime)
        {
            MoveBack(deltaTime);
        }

        private void MoveBack(float deltaTime)
        {
            _offset.y += _scrollSpeedY * deltaTime;
            _view.SetMainTextureOffset(_offset);
        }
    }
}