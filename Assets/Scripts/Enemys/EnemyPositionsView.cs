using UnityEngine;

namespace Enemys
{
    public class EnemyPositionsView : MonoBehaviour
    {
        public Transform[] SpawnPoints => _spawnPoints;

        public Transform AttackCenter => _attackCenter;

        public float AttackRadius => _attackRadius;

        public float MinDistanceBetweenPoints => _minDistanceBetweenPoints;
        
        [Header("Spawn Points")] 
        [SerializeField] private Transform[] _spawnPoints;

        [Header("Attack Zone")]
        [SerializeField] private Transform _attackCenter;
        [SerializeField] private float _attackRadius = 5f;
        [SerializeField] private float _minDistanceBetweenPoints = 1f;

#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (_attackCenter == null) return;
            UnityEditor.Handles.color = new Color(1,0,0,0.25f);
            UnityEditor.Handles.DrawSolidDisc(_attackCenter.position, Vector3.forward, _attackRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_attackCenter.position, _attackRadius);
        }
#endif
        
    }
}