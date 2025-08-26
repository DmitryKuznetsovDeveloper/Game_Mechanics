using UnityEngine;

namespace GameEngine.Objects
{
    //Нельзя менять!
    public sealed class Unit : MonoBehaviour
    {
        [SerializeField] private string type;

        [SerializeField] private int hitPoints;

        public string Type => type;

        public int HitPoints
        {
            get => hitPoints;
            set => hitPoints = value;
        }

        public Vector3 Position => transform.position;

        public Vector3 Rotation => transform.eulerAngles;

        private void Reset()
        {
            type = name;
            hitPoints = 10;
        }
    }
}