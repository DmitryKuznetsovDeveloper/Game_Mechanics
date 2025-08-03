using System.Collections.Generic;
using UnityEngine;

namespace Installers
{
    public interface IEnemyPositionService
    {
        Transform RandomSpawnPoint();
        Vector2 RandomAttackPoint();
        void ReleaseAttackPoint(Vector2 point);
    }

    public sealed class EnemyPositionService : IEnemyPositionService
    {
        readonly Transform[] _spawnPoints;
        readonly Transform _attackCenter;
        readonly float _attackRadius;
        readonly float _minDistance;

        readonly List<Vector2> _usedAttackPoints = new();

        public EnemyPositionService(
            Transform[] spawnPoints,
            Transform attackCenter,
            float attackRadius,
            float minDistance)
        {
            _spawnPoints = spawnPoints;
            _attackCenter = attackCenter;
            _attackRadius = attackRadius;
            _minDistance = minDistance;
        }

        public Transform RandomSpawnPoint()
        {
            int idx = Random.Range(0, _spawnPoints.Length);
            return _spawnPoints[idx];
        }

        public Vector2 RandomAttackPoint()
        {
            const int maxTries = 20;
            for (int i = 0; i < maxTries; i++)
            {
                var candidate = (Vector2)_attackCenter.position + Random.insideUnitCircle * _attackRadius;
                if (IsFarEnough(candidate))
                {
                    _usedAttackPoints.Add(candidate);
                    return candidate;
                }
            }

            return _attackCenter.position;
        }

        public void ReleaseAttackPoint(Vector2 point)
        {
            _usedAttackPoints.Remove(point);
        }

        bool IsFarEnough(Vector2 p)
        {
            foreach (var existing in _usedAttackPoints)
                if (Vector2.Distance(existing, p) < _minDistance)
                    return false;
            return true;
        }
    }
}