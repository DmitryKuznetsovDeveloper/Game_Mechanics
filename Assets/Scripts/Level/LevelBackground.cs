using Core;
using UnityEngine;

namespace Level
{
    [RequireComponent(typeof(Renderer))]
    public sealed class LevelBackground : MonoBehaviour, IGameUpdateListener
    {
        [SerializeField] private float _scrollSpeedY = 0.1f;

        private Material _backgroundMaterial;
        private Vector2 _offset;

        private void Awake()
        {
            ServiceLocator.Resolve<GameCycle>().AddListener(this);
            
            _backgroundMaterial = GetComponent<Renderer>().material;
        }
        
        public void OnUpdate(float deltaTime)
        {
            _offset.y += _scrollSpeedY * deltaTime;
            _backgroundMaterial.mainTextureOffset = _offset;
        }
    }
}