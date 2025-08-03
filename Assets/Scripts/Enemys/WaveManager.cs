using GameCycle;
using Installers;
using Zenject;

namespace Enemys
{
    public sealed class WaveManager : IGameTickable
    {
        readonly EnemySystem _enemySystem;
        readonly IEnemyPositionService _positions;
        readonly EnemyConfig _enemyConfig;
        readonly int _maxEnemies;

        public WaveManager(
            EnemySystem enemySystem,
            IEnemyPositionService positions,
            EnemyConfig defaultConfig,
            int maxEnemies)
        {
            _enemySystem   = enemySystem;
            _positions     = positions;
            _enemyConfig   = defaultConfig;
            _maxEnemies    = maxEnemies;
        }

        public void Tick(float deltaTime)
        {
            while (_enemySystem.CurrentEnemyCount < _maxEnemies)
            {
                SpawnOne();
            }
        }

        void SpawnOne()
        {
            var spawnPos = _positions.RandomSpawnPoint().position;
            var attackPos = _positions.RandomAttackPoint();
            _enemySystem.SpawnEnemy(spawnPos, attackPos, _enemyConfig);
        }
    }
}