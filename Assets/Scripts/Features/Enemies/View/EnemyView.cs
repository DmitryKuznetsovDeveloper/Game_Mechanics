using DG.Tweening;
using Features.Animations;
using Features.Enemies.Facade;
using UnityEngine;
using Zenject;

namespace Features.Enemies.View
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
        [SerializeField] private FlashPunchAnimator _takeDamageFlash;
        
        public void OnDestroy()
        {
            _renderer.DOKill();
            transform.DOKill();
        }
        
        public void SetPosition(Vector2 position) => transform.position = position;
        public void SetRotation(Quaternion rotation) => transform.rotation = rotation;
        
        public void PlayDamageAnimation() => _takeDamageFlash.Play();

        public class Pool : MonoMemoryPool<EnemyView> { }
    }
}