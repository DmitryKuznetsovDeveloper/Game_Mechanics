using UnityEngine;

namespace Character
{
    public class CharacterView : MonoBehaviour
    {
        public GameObject Root => gameObject;
        public Transform Transform => transform;
        public Vector2 Position => transform.position;
    }
}
