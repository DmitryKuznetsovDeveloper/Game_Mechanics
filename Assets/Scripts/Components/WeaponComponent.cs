using UnityEngine;

namespace Components
{
    /// <summary>
    /// Предоставляет позицию и ротацию точки выстрела.
    /// Автоматически ищет дочерний объект "FirePoint", если ссылка не задана в инспекторе.
    /// </summary>
    public class WeaponComponent : MonoBehaviour
    {
        [Header("Fire Point")]
        [Tooltip("Точка, из которой будет вылетать пуля")]
        [SerializeField] private Transform _firePoint;

        private void Awake()
        {
            if (_firePoint == null)
            {
                // Пытаемся найти дочерний объект с именем "FirePoint"
                var found = transform.Find("FirePoint");
                if (found != null)
                {
                    _firePoint = found;
                }
                else
                {
                    Debug.LogError($"[WeaponComponent] FirePoint не назначен и дочерний объект 'FirePoint' не найден на '{gameObject.name}'", this);
                }
            }
        }

        public Vector2 Position
        {
            get
            {
                if (_firePoint == null) return transform.position;
                return _firePoint.position;
            }
        }

        public Quaternion Rotation
        {
            get
            {
                if (_firePoint == null) return transform.rotation;
                return _firePoint.rotation;
            }
        }
    }
}