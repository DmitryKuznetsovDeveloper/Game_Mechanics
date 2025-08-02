using GameCycle;
using UnityEngine;
using Utils;
using Zenject;

namespace Enemys
{
    public sealed class WaveManager : IGameStartListener
    {
        private readonly EnemySystem _enemySystem;
        private readonly EnemyConfig _enemyConfig;

        public WaveManager(
            EnemySystem enemySystem,
            [Inject(Id = "DefaultEnemyConfig")] EnemyConfig enemyConfig)
        {
            _enemySystem = enemySystem;
            _enemyConfig = enemyConfig;
        }

        public void SpawnWave()
        {
            for (int i = 0; i < 10; i++)
            {
                Vector2 spawnPos = new Vector2(Random.Range(-7f, 7f), 8f);
                Vector2 attackPos = new Vector2(Random.Range(-7f, 7f), Random.Range(1f, 7f));
                _enemySystem.SpawnEnemy(spawnPos, attackPos, _enemyConfig);
            }
        }

        public void OnStartGame()
        {
            DebugUtil.Log("Spawning Wave");
            SpawnWave();
        }
    }
}