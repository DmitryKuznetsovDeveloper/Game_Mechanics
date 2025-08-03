using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Enemys
{
    public sealed class EnemyView :MonoBehaviour
    {
        public EnemyFacade Facade { get; set; }
        public GameObject Root => gameObject;
        public Transform Transform => transform;
        public Vector2 Position => transform.position;
        public Rigidbody2D Rigidbody => _rigidbody;
        public Transform FirePoint => _firePoint;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Transform _firePoint;
        
        public void SetPosition(Vector2 position) => transform.position = position;
        public void SetRotation(Quaternion rotation) => transform.rotation = rotation;
        
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
        
        public class Pool : MonoMemoryPool<EnemyView> { }
    }
}