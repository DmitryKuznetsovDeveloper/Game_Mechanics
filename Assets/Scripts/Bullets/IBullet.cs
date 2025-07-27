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
        public Vector2 Position;
        public Quaternion Rotation;
        public Vector2 Direction;
        public float Speed;
        public int Damage;
        public int Layer;
        public Color Color;
        public bool IsPlayer;
    }

}