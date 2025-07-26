using UnityEngine;

namespace ShootEmUp
{
    [RequireComponent(typeof(Renderer))]
    public sealed class LevelBackground : MonoBehaviour
    { 
        [SerializeField] private float _scrollSpeedY = 0.1f;

        private Material backgroundMaterial;
        private Vector2 offset;

        private void Awake()
        {
            backgroundMaterial = GetComponent<Renderer>().material;
        }

        private void Update()
        {
            offset.y += _scrollSpeedY * Time.deltaTime;
            backgroundMaterial.mainTextureOffset = offset;
        }
    }
}