using UnityEngine;
using Zenject;

namespace UI.Installer
{
    public sealed class UiOrderInitializer : IInitializable
    {
        private readonly RectTransform _popupRoot;

        public UiOrderInitializer([Inject(Id = "PopupRoot")] RectTransform popupRoot)
        {
            _popupRoot = popupRoot;
        }

        public void Initialize()
        {
            _popupRoot.SetAsLastSibling();
        }
    }
}