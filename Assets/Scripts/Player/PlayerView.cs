using DG.Tweening;
using GameCycle;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour, IGameStartListener
    {
        public PlayerFacade Facade { get; set; }
        public Rigidbody2D Rigidbody => _rigidbody;
        public Transform FirePoint => _firePoint;
        public GameObject Root => gameObject;
        public Transform Transform => transform;
        public Vector2 Position => transform.position;
        
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _firePoint;
        
        public void OnStartGame()
        {
            PlayAppearAnimation();
        }
        
        public void PlayAppearAnimation(float duration = 0.4f)
        {
            var flashColor = new Color(0f, 1f, 0.04f, 0f);
            var flashColorVisible = new Color(0f, 1f, 0.04f, 1f);
            
            transform.localScale = new Vector3(0.2f, 2.2f, 1f);
            _renderer.color = flashColor;
            
            var seq = DOTween.Sequence();
            seq.Append(_renderer.DOFade(1f, duration * 0.2f));
            seq.Join(_renderer.DOColor(flashColorVisible, 0f));
            seq.Join(transform.DOScale(new Vector3(2.4f, 0.65f, 1f), duration * 0.25f).SetEase(Ease.OutBack));
            seq.Append(transform.DOScale(new Vector3(0.8f, 1.4f, 1f), duration * 0.12f).SetEase(Ease.InOutQuad));
            seq.Append(transform.DOScale(new Vector3(1.3f, 0.8f, 1f), duration * 0.10f).SetEase(Ease.OutQuad));
            seq.Append(transform.DOScale(new Vector3(0.98f, 1.04f, 1f), duration * 0.08f).SetEase(Ease.InOutQuad));
            seq.Append(_renderer.DOColor(Color.white, duration * 0.25f));
            seq.Append(transform.DOScale(Vector3.one, duration * 0.15f).SetEase(Ease.OutElastic));
            seq.Append(transform.DOPunchScale(Vector3.one * 0.25f, 0.22f, 12, 1f));
        }
        
        public void PlayDamageAnimation()
        {
            _renderer.DOKill();
            transform.DOKill();
            var origColor = _renderer.color;
            var flashDur = 0.1f;
            var punchDur = 0.2f;
            var seq = DOTween.Sequence();
            seq.Append(_renderer.DOColor(Color.red, flashDur * 0.5f));
            seq.Join(transform.DOPunchScale(Vector3.one * 0.2f, punchDur, 10, 1f)
                .SetEase(Ease.OutQuad));
            seq.Append(_renderer.DOColor(origColor, flashDur * 0.5f));
        }
    }
}
