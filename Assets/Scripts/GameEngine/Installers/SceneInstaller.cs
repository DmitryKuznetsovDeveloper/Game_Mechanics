using System.Collections.Generic;
using GameEngine.Objects;
using GameEngine.Providers;
using UnityEngine;
using Zenject;
using GameEngine.Systems;
using GameEngine.SaveModule;

namespace GameEngine.Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [Header("Scene References")]
        [SerializeField] private Transform _unitContainer;

        public override void InstallBindings()
        {
   
            Container.Bind<Transform>()
                .WithId("UnitContainer")
                .FromInstance(_unitContainer);
            
            Container.Bind<IUnitPrefabProvider>()
                .To<UnitPrefabProvider>()
                .AsSingle();
            
            Container.Bind<UnitManager>()
                .AsSingle();
            
            Container.Bind<ResourceService>()
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<SceneSetupSystem>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<Test>()
                .FromComponentsInHierarchy()
                .AsSingle();
            
            Container.BindInterfacesTo<UnitsSaveModule>().AsSingle().NonLazy();
            Container.BindInterfacesTo<ResourcesSaveModule>().AsSingle().NonLazy();
        }
    }
}