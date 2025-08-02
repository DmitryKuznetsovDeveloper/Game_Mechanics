using System;
using UnityEngine;

namespace Bullets
{
    public interface IBullet
    {
        void Initialize(BulletData data);
        void Move();
        void Stop();
        event Action<IBullet, GameObject> OnCollision;
     
        int Damage { get; }
        bool IsPlayer { get; }
    }
    
    public struct BulletData
    {
        public Vector2 Position;
        public Quaternion Rotation;
        public Vector2 Direction;
        public int LifeTime;
        public float Speed;
        public int Damage;
        public int Layer;
        public Color Color;
        public bool IsPlayer;
    }

}