using UI.View;
using UI.ViewModel;
using UnityEngine;
using Zenject;

public sealed class PlayerSelectorBinder : MonoBehaviour
{
    [SerializeField] private PlayerSelectorView _view;

    private IPlayerListViewModel _vm;

    [Inject]
    private void Construct(IPlayerListViewModel vm)
    {
        _vm = vm;
    }

    private void OnEnable()
    {
        // Подписки View -> VM
        _view.NextClicked   += _vm.GoNext;
        _view.PrevClicked   += _vm.GoPrev;
        _view.SelectClicked += _vm.Select;

        // Подписка VM -> View
        _vm.OnPlayerSelected += HandlePlayerSelected;

        // Первичное заполнение
        HandlePlayerSelected(_vm.CurrentPlayerSelected);
    }

    private void OnDisable()
    {
        _view.NextClicked   -= _vm.GoNext;
        _view.PrevClicked   -= _vm.GoPrev;
        _view.SelectClicked -= _vm.Select;
        _vm.OnPlayerSelected -= HandlePlayerSelected;
    }

    private void HandlePlayerSelected(IPlayerViewModel p)
    {
        _view.Render(p);
    }
}