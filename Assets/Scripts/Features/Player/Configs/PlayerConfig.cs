using Features.Bullets.Configs;
using UnityEngine;

namespace Features.Player.Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig", order = 0)]
    public sealed class PlayerConfig : ScriptableObject
    {
        [SerializeField] private int _maxHitPoints;
        [SerializeField] private float _speed;
        [SerializeField] private float _minMovePositionX;
        [SerializeField] private float _maxMovePositionX;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private bool _isPlayer;
        
        public int MaxHitPoints => _maxHitPoints;
        public float Speed => _speed;
        public float MinMovePositionX => _minMovePositionX;
        public float MaxMovePositionX => _maxMovePositionX;
        public BulletConfig BulletConfig => _bulletConfig;
        public bool IsPlayer => _isPlayer;
    }
}