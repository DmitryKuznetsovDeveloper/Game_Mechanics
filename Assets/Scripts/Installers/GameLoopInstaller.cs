using Player;
using UnityEngine;
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

        public override void InstallBindings()
        {
            // Player
            var playerInstaller = new PlayerInstaller(_playerConfig, _playerSpawnPoint);
            playerInstaller.InstallBindings(Container);

            var uiInstaller = new UIInstaller(_countdownTime,_uiSpawnPoint);
            uiInstaller.InstallBindings(Container);
        }
    }
}