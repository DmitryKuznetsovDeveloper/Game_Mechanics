using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyPositions : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private Transform[] _spawnPoints;

        [Header("Attack Zone")]
        [SerializeField] private Transform _attackCenter;
        [SerializeField] private float _attackRadius = 5f;
        [SerializeField] private float _minDistanceBetweenPoints = 1f;

        private readonly List<Vector2> _usedAttackPoints = new();

        public Transform RandomSpawnPosition()
        {
            var index = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[index];
        }

        public Vector2 RandomAttackPosition()
        {
            const int maxTries = 20;
            for (int i = 0; i < maxTries; i++)
            {
                var randomOffset = Random.insideUnitCircle * _attackRadius;
                var point = (Vector2)_attackCenter.position + randomOffset;

                if (!IsFarEnoughFromOthers(point)) 
                    continue;
                
                _usedAttackPoints.Add(point);
                return point;
            }
            return _attackCenter.position;
        }
        
        public void ReleaseAttackPosition(Vector2 point)
        {
            _usedAttackPoints.Remove(point);
        }

        private bool IsFarEnoughFromOthers(Vector2 point)
        {
            foreach (var existing in _usedAttackPoints)
            {
                if (Vector2.Distance(existing, point) < _minDistanceBetweenPoints)
                    return false;
            }
            return true;
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (_attackCenter != null)
            {
                UnityEditor.Handles.color = new Color(1f, 0f, 0f, 0.25f);
                UnityEditor.Handles.DrawSolidDisc(_attackCenter.position, Vector3.forward, _attackRadius);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_attackCenter.position, _attackRadius);
            }
        }
#endif
    }
}