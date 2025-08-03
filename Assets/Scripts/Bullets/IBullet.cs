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
         BulletView View  { get; }
        int Damage { get; }
        bool IsPlayer { get; }
    }

    public struct BulletData
    {
        public Vector2 Position;
        public Vector2 Direction;
        public Quaternion Rotation;
        public bool IsPlayer;
        public BulletConfig Config;
    }
}