using System;
using System.Collections.Generic;
using System.Threading;
using Core.Utilities;
using Cysharp.Threading.Tasks;
using UI.Installer;
using UI.Popup;
using UnityEngine;
using Zenject;

namespace UI.Popup
{
    public sealed class PopupService : IPopupService
    {
        private readonly DiContainer _container;
        private readonly Transform _popupRoot;
        
        private readonly Dictionary<string, BasePopup> _prefabsByKey;
        private readonly Dictionary<Type, BasePopup> _prefabsByType;
        private readonly Dictionary<Type, string> _keyByType = new();
        private readonly Dictionary<string, BasePopup> _instancesByKey = new();
        private readonly Dictionary<Type, BasePopup> _instancesByType = new();

        public PopupService(
            DiContainer container,
            [Inject(Id = "PopupRoot")] RectTransform popupRoot,
            IEnumerable<PopupEntry> entries)
        {
            _container = container;
            _popupRoot = popupRoot;

            _prefabsByKey = new Dictionary<string, BasePopup>(StringComparer.Ordinal);
            _prefabsByType = new Dictionary<Type, BasePopup>();

            if (_popupRoot == null)
                DebugUtil.LogError("[PopupService] _popupRoot is null — назначь корневой контейнер для попапов.");

            if (entries == null)
                return;

            foreach (var e in entries)
            {
                if (e == null) 
                    continue;

                var key = e.Key;
                var prefab = e.Prefab;
                if (prefab == null) 
                    continue;
                
                if (!string.IsNullOrWhiteSpace(key))
                {
                    if (!_prefabsByKey.TryAdd(key, prefab))
                    {
                        DebugUtil.LogWarning($"[PopupService] Duplicate key '{key}'. Игнорирую повторную запись.");
                    }
                }
                
                var type = prefab.GetType();
                if (!_prefabsByType.TryAdd(type, prefab))
                {
                    DebugUtil.LogWarning(
                        $"[PopupService] Type '{type.Name}' уже зарегистрирован. Оставляю первый префаб, этот игнорирую.");
                }
                
                if (!string.IsNullOrWhiteSpace(key))
                {
                    _keyByType.TryAdd(type, key);
                }
            }
        }

        public async UniTask<T> Get<T>(bool createIfMissing = true) where T : BasePopup
        {
            var type = typeof(T);

            if (_instancesByType.TryGetValue(type, out var cached) && cached != null)
                return (T)cached;

            if (!createIfMissing)
                return null;

            if (!_prefabsByType.TryGetValue(type, out var prefab) || prefab == null)
                throw new Exception($"Popup prefab for type '{type.Name}' is not registered in PopupInstaller.");

            var instance = _container.InstantiatePrefabForComponent<T>(prefab, _popupRoot);
            Normalize(instance);

            _instancesByType[type] = instance;
            
            if (_keyByType.TryGetValue(type, out var key) && !string.IsNullOrEmpty(key))
                _instancesByKey[key] = instance;

            await UniTask.Yield();
            return instance;
        }
        
        public async UniTask Show<T>(CancellationToken token = default) where T : BasePopup
        {
            var popup = await Get<T>(true);
            await popup.ShowAsync(token);
        }
        
        public async UniTask Hide<T>(CancellationToken token = default) where T : BasePopup
        {
            var popup = await Get<T>(false);
            
            if (popup == null) 
                return;
            
            await popup.HideAsync(token);
        }
        
        public async UniTask Show(string key, CancellationToken token = default)
        {
            var popup = await GetByKey(key, true);
            await popup.ShowAsync(token);
        }
        
        public async UniTask Hide(string key, CancellationToken token = default)
        {
            var popup = await GetByKey(key, false);
            
            if (popup == null) 
                return;
            
            await popup.HideAsync(token);
        }
        
        public async UniTask HideAll(CancellationToken token = default)
        {
            foreach (var inst in _instancesByType.Values)
            {
                if (inst == null) 
                    continue;
                
                if (!inst.IsVisible && !inst.gameObject.activeSelf) 
                    continue;

                token.ThrowIfCancellationRequested();

                try
                {
                    await inst.HideAsync(token);
                }
                catch (OperationCanceledException)
                {
                    inst.InstantHide();
                    throw;
                }
            }
        }

        public bool IsVisible<T>() where T : BasePopup => 
            _instancesByType.TryGetValue(typeof(T), out var inst) && inst != null && inst.IsVisible;

        public bool IsVisible(string key) => 
            _instancesByKey.TryGetValue(key, out var inst) && inst != null && inst.IsVisible;

        private async UniTask<BasePopup> GetByKey(string key, bool createIfMissing)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Popup key is null/empty.", nameof(key));

            if (_instancesByKey.TryGetValue(key, out var cached) && cached != null)
                return cached;

            if (!createIfMissing)
                return null;

            if (!_prefabsByKey.TryGetValue(key, out var prefab) || prefab == null)
                throw new Exception($"Popup prefab for key '{key}' is not registered in PopupInstaller.");
            
            var instance = _container.InstantiatePrefabForComponent<BasePopup>(prefab, _popupRoot);
            Normalize(instance);

            _instancesByKey[key] = instance;
            _instancesByType[instance.GetType()] = instance;

            await UniTask.Yield();
            return instance;
        }

        private static void Normalize(BasePopup popup)
        {
            var rt = popup.Root;
            
            if (!rt) 
                return;

            rt.SetParent(rt.parent, worldPositionStays: false);
            rt.anchoredPosition3D = Vector3.zero;
            rt.localRotation = Quaternion.identity;
            rt.localScale = Vector3.one;
        }
    }
}


[Serializable]
public sealed class PopupEntry
{
    public string Key;
    public BasePopup Prefab;
}