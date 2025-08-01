using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(menuName = "Configs/BulletConfig")]
    public sealed class BulletConfig : ScriptableObject
    {
        [Header("Main Parameters")] 
        [SerializeField] private float _speed = 10f; 
        [SerializeField] private int _damage = 1; 
        [SerializeField] private PhysicsLayer _physicsLayer; 
        [SerializeField] private Color _color = Color.white;
        
        public float Speed => _speed;
        public int Damage => _damage;
        public PhysicsLayer PhysicsLayer => _physicsLayer;
        public Color Color => _color;
    }
}