using UnityEngine;
using Zenject;

namespace Enemys
{
    public sealed class EnemyView :MonoBehaviour
    {
        public Transform FirePoint => _firePoint;
        public GameObject Root => gameObject;
        public Transform Transform => transform;
        public Vector2 Position => transform.position;
        
        [SerializeField] private Transform _firePoint;
        
        public class Pool : MonoMemoryPool<EnemyView> { }
    }
}