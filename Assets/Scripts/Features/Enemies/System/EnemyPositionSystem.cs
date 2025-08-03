using System.Collections.Generic;
using Features.Enemies.View;
using UnityEngine;

namespace Features.Enemies.System
{
    public interface IEnemyPositionSystem
    {
        Transform RandomSpawnPoint();
        Vector2 RandomAttackPoint();
        void ReleaseAttackPoint(Vector2 point);
    }

    public sealed class EnemyPositionSystem : IEnemyPositionSystem
    {
        private readonly EnemyPositionsView _enemyPositionsView;

        readonly List<Vector2> _usedAttackPoints = new();

        public EnemyPositionSystem(EnemyPositionsView enemyPositionsView)
        {
            _enemyPositionsView = enemyPositionsView;
        }


        public Transform RandomSpawnPoint()
        {
            var spawnPoints = _enemyPositionsView.SpawnPoints;
            int idx = Random.Range(0, spawnPoints.Length);
            return spawnPoints[idx];
        }

        public Vector2 RandomAttackPoint()
        {
            const int maxTries = 20;
            for (var i = 0; i < maxTries; i++)
            {
                var candidate = _enemyPositionsView.AttackCenterPosition + Random.insideUnitCircle * _enemyPositionsView.AttackRadius;
                
                if (!IsFarEnough(candidate)) 
                    continue;
                
                _usedAttackPoints.Add(candidate);
                return candidate;
            }

            return _enemyPositionsView.AttackCenterPosition;
        }

        public void ReleaseAttackPoint(Vector2 point)
        {
            _usedAttackPoints.Remove(point);
        }

        bool IsFarEnough(Vector2 p)
        {
            foreach (var existing in _usedAttackPoints)
                if (Vector2.Distance(existing, p) < _enemyPositionsView.MinDistanceBetweenPoints)
                    return false;
            return true;
        }
    }
}