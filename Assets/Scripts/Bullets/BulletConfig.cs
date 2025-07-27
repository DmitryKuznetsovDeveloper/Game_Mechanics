using UnityEngine;

namespace Bullets
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "ShootEmUp/BulletConfig")]
    public sealed class BulletConfig : ScriptableObject
    {
        [Header("Physics")]
        [Tooltip("Слой физики для пули")]
        public PhysicsLayer PhysicsLayer;

        [Header("Visual")]
        [Tooltip("Цвет спрайта пули")]
        public Color Color = Color.white;

        [Header("Combat")]
        [Tooltip("Скорость полёта пули")]
        public float Speed = 10f;

        [Tooltip("Урон, наносимый пулей")]
        public int Damage = 1;
    }
}