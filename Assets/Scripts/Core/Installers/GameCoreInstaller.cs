using Core.GameCycle;
using Core.Shared.Components;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    [CreateAssetMenu(fileName = "GameCoreInstaller", menuName = "Installers/GameCoreInstaller")]
    public class GameCoreInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<GameManager>().AsSingle();
            Container.Bind<TimerComponent>().AsSingle();
        }
    }
}