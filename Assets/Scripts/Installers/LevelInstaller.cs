using Level;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Renderer _backgroundMaterial;
        [SerializeField] private float _scrollSpeedY;
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<LevelBackground>().FromNew().AsSingle()
                .WithArguments(_backgroundMaterial.sharedMaterial, _scrollSpeedY);
        }
    }
}