using Components;
using Core;
using GameCycle;
using UnityEngine;
using Zenject;

namespace Installers
{
    [CreateAssetMenu(fileName = "GameCoreInstaller", menuName = "Installers/GameCoreInstaller")]
    public class GameCoreInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            //Game Cycle
            Container.Bind<GameManager>().AsSingle();
            Container.BindInterfacesTo<GameController>().AsSingle();
            
            Container.Bind<TimerComponent>()
                .AsSingle();
        }
    }
}