using Features.Bullets.Collision;
using UnityEngine;

namespace Features.Bullets.Configs
{
    [CreateAssetMenu(menuName = "Configs/BulletConfig")]
    public sealed class BulletConfig : ScriptableObject
    {
        [Header("Main Parameters")] 
        [SerializeField] private int _lifeTime = 10; 
        [SerializeField] private float _speed = 10f; 
        [SerializeField] private int _damage = 1; 
        [SerializeField] private PhysicsLayer _physicsLayer; 
        [SerializeField] private Color _color = Color.white;
        
        public int Lifetime => _lifeTime;
        public int Damage => _damage;
        public float Speed => _speed;
        public PhysicsLayer PhysicsLayer => _physicsLayer;
        public Color Color => _color;
    }
}