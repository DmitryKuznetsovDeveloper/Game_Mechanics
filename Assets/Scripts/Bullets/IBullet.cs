using System;
using UnityEngine;

namespace Bullets
{
    public interface IBullet
    {
        void Initialize(BulletData data);
        void Fire();
        void Stop();
        event Action<IBullet, GameObject> OnCollision;
        event Action<IBullet, GameObject> OnTriggerEnter;
    }
    
    public struct BulletData
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public float Speed;
        public int Damage;
        public int Layer;
        public Color Color;
        public bool IsPlayer;
    }
}