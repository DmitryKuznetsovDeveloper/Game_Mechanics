using Bullets;
using UnityEngine;
using Components;

namespace Enemys
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 1)]
    public class EnemyConfig : ScriptableObject
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