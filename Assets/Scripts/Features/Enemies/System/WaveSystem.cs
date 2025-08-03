using Core.GameCycle;
using Features.Enemies.Configs;

namespace Features.Enemies.System
{
    public sealed class WaveSystem : IGameTickable
    {
        readonly EnemySystem _enemySystem;
        readonly IEnemyPositionSystem _positions;
        readonly EnemyConfig _enemyConfig;
        readonly int _maxEnemies;

        public WaveSystem(
            EnemySystem enemySystem,
            IEnemyPositionSystem positions,
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