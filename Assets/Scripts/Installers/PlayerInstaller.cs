using Input;
using Player;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class PlayerInstaller
    {
        private readonly PlayerConfig _playerConfig;
        private readonly Transform _spawnPoint;
        private const string CHARACTER_PREFAB_PATH = "Objects/Player";

        public PlayerInstaller(PlayerConfig config, Transform spawnPoint)
        {
            _playerConfig = config;
            _spawnPoint = spawnPoint;
        }

        public void InstallBindings(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<PlayerView>()
                .FromComponentInNewPrefabResource(CHARACTER_PREFAB_PATH)
                .UnderTransform(_spawnPoint)
                .AsSingle()
                .NonLazy();

            container.Bind<PlayerFacade>()
                .FromNew()
                .AsSingle()
                .WithArguments(_playerConfig)
                .NonLazy();
            
            container.BindInterfacesAndSelfTo<InputReader>().FromNew().AsSingle();
            container.BindInterfacesAndSelfTo<PlayerInputController>().FromNew().AsSingle().WithArguments(_playerConfig);
        }
    }
}