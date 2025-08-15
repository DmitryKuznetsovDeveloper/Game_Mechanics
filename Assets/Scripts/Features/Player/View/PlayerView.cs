using Core.GameCycle;
using DG.Tweening;
using Features.Animations;
using Features.Player.Facade;
using UnityEngine;

namespace Features.Player.View
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
        [SerializeField] private FlashPunchAnimator _takeDamageFlash;
        [SerializeField] private AppearAnimator _appearAnimator;

        private Color _baseColor;
        private Vector3 _baseScale;
        private Sequence _damageSeq;

        public void OnStartGame() => _appearAnimator.Play();
        
        public void PlayDamageAnimation() => _takeDamageFlash.Play();
    }
}