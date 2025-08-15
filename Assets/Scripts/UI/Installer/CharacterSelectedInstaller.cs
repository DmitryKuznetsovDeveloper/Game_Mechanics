using System.Collections.Generic;
using UI.Popup;
using UI.System;
using UI.View;
using UI.ViewModel;
using UnityEngine;
using Zenject;

namespace UI.Installer
{
    public sealed class CharacterSelectedInstaller : MonoInstaller<CharacterSelectedInstaller>
    {
        [Header("Scene Refs")]
        [SerializeField] private Transform _metaUiSpawnPoint;
        [SerializeField] private List<PopupEntry> _entries;

        private const string UI_META_PATH = "UI/UIMeta";
        private const string UI_POPUP_CANVAS_PATH = "UI/PopupCanvas";

        public override void InstallBindings()
        {
            Container.Bind<IMetaUIView>()
                .To<MetaUIView>()
                .FromComponentInNewPrefabResource(UI_META_PATH)
                .UnderTransform(_metaUiSpawnPoint)
                .AsSingle();

            Container.Bind<RectTransform>()
                .WithId("PopupRoot")
                .FromComponentInNewPrefabResource(UI_POPUP_CANVAS_PATH)
                .UnderTransform(_metaUiSpawnPoint)
                .AsSingle();
            
            Container.BindInterfacesAndSelfTo<PopupService>()
                .AsSingle()
                .WithArguments(_entries);
            
            Container.BindInterfacesAndSelfTo<SelectedPlayerProvider>().AsSingle();

            Container.Bind<IMetaUIViewModel>()
                .To<MetaUIViewModel>()
                .AsSingle()
                .NonLazy();

            Container.Bind<IPlayerListViewModel>()
                .To<PlayerListViewModel>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesTo<UiOrderInitializer>().AsSingle().NonLazy();
        }
    }
}