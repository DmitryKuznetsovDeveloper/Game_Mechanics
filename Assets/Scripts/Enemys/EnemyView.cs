using UnityEngine;
using Zenject;

namespace Enemys
{
    public sealed class EnemyView :MonoBehaviour
    {
        public GameObject Root => gameObject;
        public Transform Transform => transform;
        public Vector2 Position => transform.position;
        public Rigidbody2D Rigidbody => _rigidbody;
        public Transform FirePoint => _firePoint;
        
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _firePoint;
        
        public class Pool : MonoMemoryPool<EnemyView> { }
    }
}