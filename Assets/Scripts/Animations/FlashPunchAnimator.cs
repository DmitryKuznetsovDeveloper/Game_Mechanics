using DG.Tweening;
using UnityEngine;

namespace Features.Animations
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class FlashPunchAnimator : MonoBehaviour
    { 
        [SerializeField] private Color _flashColor = Color.red;
        [SerializeField] private float _flashDuration = 0.1f;
        [SerializeField] private Vector3 _punchStrength = Vector3.one * 0.2f;
        [SerializeField] private float _punchDuration = 0.2f;
        [SerializeField] private int _punchVibrato = 10;
        [SerializeField] private float _punchElasticity = 1f;

        private SpriteRenderer _renderer;
        private Vector3 _baseScale;
        private Color _baseColor;
        private Sequence _seq;

       private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _baseColor = _renderer.color;
            _baseScale = transform.localScale;
            BuildSequence();
        }

        private void OnDestroy()
        {
            _seq?.Kill();
            _renderer.DOKill();
            transform.DOKill();
        }

        public void Play()
        {
            if (_seq == null) return;
            ResetState();
            _seq.Restart();
        }
        
        private void BuildSequence()
        {
            _renderer.DOKill();
            transform.DOKill();

            _seq = DOTween.Sequence()
                .Append(_renderer.DOColor(_flashColor, _flashDuration * 0.5f))
                .Join(transform.DOPunchScale(_punchStrength, _punchDuration, _punchVibrato, _punchElasticity)
                    .SetEase(Ease.OutQuad))
                .Append(_renderer.DOColor(_baseColor, _flashDuration * 0.5f))
                .SetAutoKill(false)
                .Pause()
                .OnComplete(ResetState);
        }

        private void ResetState()
        {
            _renderer.color = _baseColor;
            transform.localScale = _baseScale;
        }
    }
}