using Features.Bullets.Configs;
using UnityEngine;

namespace Features.Enemies.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 1)]
    public sealed class EnemyConfig : ScriptableObject
    {
        [SerializeField] private int _maxHitPoints;
        [SerializeField] private float _speed;
        [SerializeField] private bool _isPlayer;
        [SerializeField] private BulletConfig _bulletConfig;

        [SerializeField] private float _stopDistance;
        [SerializeField] private float _fireCooldown;
        
        public int MaxHitPoints => _maxHitPoints;

        public float Speed => _speed;

        public bool IsPlayer => _isPlayer;

        public BulletConfig BulletConfig => _bulletConfig;

        public float StopDistance => _stopDistance;

        public float FireCooldown => _fireCooldown;
    }
}