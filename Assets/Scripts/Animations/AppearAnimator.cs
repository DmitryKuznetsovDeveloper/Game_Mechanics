using DG.Tweening;
using UnityEngine;

namespace Features.Animations
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AppearAnimator : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float _duration = 0.5f;

        private SpriteRenderer _renderer;
        private Vector3 _baseScale;
        private Color _baseColor;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _baseColor = _renderer.color;
            _baseScale = transform.localScale;
            
            transform.localScale = Vector3.zero;
            var hiddenColor = _baseColor;
            hiddenColor.a = 0f;
            _renderer.color = hiddenColor;
        }
        
        public void Play()
        {
            transform.localScale = Vector3.zero;
            var startColor = _baseColor;
            startColor.a = 0f;
            _renderer.color = startColor;
            
            transform
                .DOScale(_baseScale, _duration)
                .SetEase(Ease.OutBack);
            
            _renderer
                .DOFade(1f, _duration * 0.6f)
                .SetEase(Ease.Linear);
            
            transform
                .DOPunchScale(_baseScale * 0.1f, _duration * 0.4f, 5, 0.5f)
                .SetDelay(_duration)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _renderer.color = _baseColor;
                    transform.localScale = _baseScale;
                });
        }
    }
}