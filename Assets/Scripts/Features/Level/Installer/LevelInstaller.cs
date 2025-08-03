using Features.Level.System;
using Features.Level.View;
using UnityEngine;
using Zenject;

namespace Features.Level.Installer
{
    public sealed class LevelInstaller 
    {
        private readonly Transform _spawnPoint;
        private float _scrollSpeedY;

        public LevelInstaller(Transform spawnPoint, float scrollSpeedY)
        {
            _spawnPoint = spawnPoint;
            _scrollSpeedY = scrollSpeedY;
        }

        private const string BACKGROUND_PREFAB_PATH = "Level/BackgroundScroller";
        public void InstallBindings(DiContainer container)
        {
            container.Bind<BackgroundScrollerView>()
                .FromComponentInNewPrefabResource(BACKGROUND_PREFAB_PATH)
                .UnderTransform(_spawnPoint)
                .AsSingle()
                .NonLazy();
            
            container.BindInterfacesAndSelfTo<BackgroundScroller>().FromNew().AsSingle().WithArguments(_scrollSpeedY);
        }
    }
}