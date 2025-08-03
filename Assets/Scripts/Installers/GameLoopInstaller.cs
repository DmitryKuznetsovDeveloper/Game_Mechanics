using Enemys;
using Player;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class GameLoopInstaller : MonoInstaller
    {
        [Header("Character Settings")] 
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private Transform _playerSpawnPoint;

        [Header("UI Settings")] 
        [SerializeField] private int _countdownTime;
        [SerializeField] private Transform _uiSpawnPoint;

        [Header("Enemy Settings")] 
        [SerializeField] private EnemyConfig _defaultConfig;
        [SerializeField] private Transform _enemyContainer;
        [SerializeField] private EnemyPositionsView _enemyPositionsView;
        [SerializeField] private int _maxEnemies;

        [Header("Bullet Settings")] [SerializeField]
        private Transform _bulletSpawnPoint;

        public override void InstallBindings()
        {
            // Player
            var playerInstaller = new PlayerInstaller(_playerConfig, _playerSpawnPoint);
            playerInstaller.InstallBindings(Container);

            //Bullet
            var bulletSystem = new BulletSystemInstaller(_bulletSpawnPoint);
            bulletSystem.InstallBindings(Container);

            //Enemys
            var enemyInstaller = new EnemyInstaller(_defaultConfig, _enemyContainer,_enemyPositionsView,_maxEnemies);
            enemyInstaller.InstallBindings(Container);

            //UI
            var uiInstaller = new UIInstaller(_countdownTime, _uiSpawnPoint);
            uiInstaller.InstallBindings(Container);
        }
    }
}