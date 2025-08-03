using Features.Bullets.Installer;
using Features.Enemies.Configs;
using Features.Enemies.Installer;
using Features.Enemies.View;
using Features.Level.Installer;
using Features.Player.Configs;
using Features.Player.Installer;
using UI.Installer;
using UnityEngine;
using Zenject;

namespace Core.Installers
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
        [SerializeField] private int _maxEnemies;

        [Header("Bullet Settings")] 
        [SerializeField] private Transform _bulletSpawnPoint;
        
        [Header("Level Settings")] 
        [SerializeField] private Transform _backgroundSpawnPoint;
        [SerializeField] private float _speedScrollBack;

        public override void InstallBindings()
        {
            //Level
            var levelInstaller = new LevelInstaller(_backgroundSpawnPoint, _speedScrollBack);
            levelInstaller.InstallBindings(Container);
            
            // Player
            var playerInstaller = new PlayerInstaller(_playerConfig, _playerSpawnPoint);
            playerInstaller.InstallBindings(Container);

            //Bullet
            var bulletSystem = new BulletSystemInstaller(_bulletSpawnPoint);
            bulletSystem.InstallBindings(Container);

            //Enemys
            var enemyInstaller = new EnemyInstaller(_defaultConfig, _enemyContainer,_maxEnemies);
            enemyInstaller.InstallBindings(Container);

            //UI
            var uiInstaller = new UIInstaller(_countdownTime, _uiSpawnPoint);
            uiInstaller.InstallBindings(Container);
        }
    }
}