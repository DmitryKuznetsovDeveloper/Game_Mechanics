using GameCycle;
using UnityEngine;

namespace Level
{
    public sealed class LevelBackground : IGameTickable
    {
        private readonly Material _backgroundMaterial;
        private readonly float _scrollSpeedY;
        private Vector2 _offset;

        public LevelBackground(Material backgroundMaterial, float scrollSpeedY)
        {
            _backgroundMaterial = backgroundMaterial;
            _scrollSpeedY = scrollSpeedY;
        }
        
        public void Tick(float deltaTime)
        {
            MoveBack(deltaTime);
        }

        private void MoveBack(float deltaTime)
        {
            _offset.y += _scrollSpeedY * deltaTime;
            _backgroundMaterial.mainTextureOffset = _offset;
        }
    }
}