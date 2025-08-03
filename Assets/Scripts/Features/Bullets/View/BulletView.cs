using System;
using Features.Animations;
using Features.Bullets.Collision;
using UnityEngine;
using Zenject;

namespace Features.Bullets.View
{
    public sealed class BulletView : MonoBehaviour
    {
        public IBullet Facade { get; set; }
        public event Action<Collision2D> OnCollisionEnter;
        
        public GameObject Root => gameObject;
        public Transform Transform => transform;
        public Vector2 Position => transform.position;
        public Rigidbody2D Rigidbody => _rigidbody;
        public SpriteRenderer Renderer => _renderer;

        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private SpriteRenderer _renderer;

        public void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEnter?.Invoke(collision);
        }
        
        public void SetPosition(Vector2 position) => transform.position = position;
        
        public void SetRotation(Quaternion rotation) => transform.rotation = rotation;
        
        public void SetColor(Color color) => _renderer.color = color;
        
        public void SetLayer(PhysicsLayer physicsLayer) => gameObject.layer = (int)physicsLayer;
        
        public class Pool : MonoMemoryPool<BulletView> { }
    }
}