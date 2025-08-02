using Bullets;
using UnityEngine;
using Components;

namespace Enemys
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig", order = 1)]
    public class EnemyConfig : ScriptableObject
    {
        public float MoveSpeed = 2f;
        public int MaxHitPoints = 10;
        public float StopDistance = 0.3f;
        public float FireCooldown = 1.0f;
        public BulletConfig BulletConfig;
        public bool IsPlayer;
    }
}