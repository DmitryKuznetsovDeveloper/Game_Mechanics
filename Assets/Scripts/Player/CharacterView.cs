using UnityEngine;

namespace Player
{
    public class CharacterView : MonoBehaviour
    {
        public GameObject Root => gameObject;
        public Transform Transform => transform;
        public Vector2 Position => transform.position;
    }
}
