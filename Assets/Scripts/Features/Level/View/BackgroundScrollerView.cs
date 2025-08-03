using UnityEngine;

namespace Features.Level.View
{
    public class BackgroundScrollerView : MonoBehaviour
    {
        [SerializeField] private Renderer _backgroundMaterial;
        
        public void SetMainTextureOffset(Vector2 offset) => 
            _backgroundMaterial.sharedMaterial.mainTextureOffset = offset;
    }
}