using UnityEngine;
using UI;
using Zenject;

namespace Installers
{
    public sealed class UIInstaller 
    {
        private readonly int _countdownTime;
        private readonly Transform _uiSpawnPoint;
        private const string UI_PREFAB_PATH = "UI/UIGame";
        
        public UIInstaller(int countdownTime, Transform uiSpawnPoint)
        {
            _countdownTime = countdownTime;
            _uiSpawnPoint = uiSpawnPoint;
        }

        public void InstallBindings(DiContainer container)
        {
            container.Bind<GameUIView>()
                .FromComponentInNewPrefabResource(UI_PREFAB_PATH)
                .UnderTransform(_uiSpawnPoint)
                .AsSingle();
            
            container.BindInterfacesAndSelfTo<GameUIController>()
                .AsSingle();
            
            container.BindInstance(_countdownTime)
                .WithId("UI_CountdownTime");
        }
    }
}