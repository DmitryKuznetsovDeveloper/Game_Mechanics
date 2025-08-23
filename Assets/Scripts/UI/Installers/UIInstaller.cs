using UI.ViewModels;
using Zenject;

namespace UI.Installers
{
    public class UIInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveSystemView>().To<SaveSystemView>()
                .FromComponentInHierarchy()
                .AsSingle();

            Container.Bind<SaveSystemViewModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}