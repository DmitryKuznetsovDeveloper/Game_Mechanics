using System;
using Cysharp.Threading.Tasks;
using UI.Popup;
using UI.System;
using UI.View;
using Zenject;

namespace UI.ViewModel
{
    public sealed class MetaUIViewModel : IMetaUIViewModel, IDisposable
    {
        private readonly IPopupService _popupService;
        private readonly ISelectedPlayerProvider _selectedPlayerProvider;
        private readonly IMetaUIView _view;

        [Inject]
        public MetaUIViewModel(
            IPopupService popupService,
            ISelectedPlayerProvider selectedPlayerProvider,
            IMetaUIView view)
        {
            _popupService = popupService;
            _selectedPlayerProvider = selectedPlayerProvider;
            _view = view;

            _view.OnSelectCharacterClicked += HandleOpenPopup;
            _selectedPlayerProvider.OnChanged += HandleSelectionChanged;

            Refresh();
        }

        private void HandleOpenPopup()
        {
            _popupService.Show<PlayerSelectorView>().Forget();
        }

        private void HandleSelectionChanged(IPlayerViewModel _)
        {
            Refresh();
        }

        private void Refresh()
        {
            var text = _selectedPlayerProvider.Current != null
                ? $"Выбран: {_selectedPlayerProvider.Current.Name}"
                : "Персонаж не выбран";
            _view.SetSelectedText(text);
        }

        public void Dispose()
        {
            _view.OnSelectCharacterClicked -= HandleOpenPopup;
            _selectedPlayerProvider.OnChanged -= HandleSelectionChanged;
        }
    }
}