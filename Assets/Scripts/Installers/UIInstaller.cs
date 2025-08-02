using Components;
using GameCycle;
using UnityEngine;
using UI;
using Zenject;

namespace Installers
{
    public class UIInstaller : MonoInstaller
    {
        [Header("Countdown Settings")] 
        [SerializeField] int _countdownTime = 3;

        public override void InstallBindings()
        {
            // View (должен лежать в сцене)
            Container.Bind<GameUIView>()
                .FromComponentInHierarchy()
                .AsSingle();

            // Контроллер UI
            Container.BindInterfacesAndSelfTo<GameUIController>()
                .AsSingle();

            // Время обратного отсчёта
            Container.BindInstance(_countdownTime)
                .WithId("UI_CountdownTime");
        }
    }
}