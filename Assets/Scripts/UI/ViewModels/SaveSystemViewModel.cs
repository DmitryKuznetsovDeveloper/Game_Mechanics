using System;
using GameEngine.Systems;

namespace UI.ViewModels
{
    public class SaveSystemViewModel : IDisposable
    {
        private readonly ISaveSystemView _view;
        private readonly IGameSaveSystem _saveSystem;

        public SaveSystemViewModel(ISaveSystemView view, IGameSaveSystem saveSystem)
        {
            _view = view;
            _saveSystem = saveSystem;

            _view.OnSaveClicked += HandleSave;
            _view.OnLoadClicked += HandleLoad;
        }
        
        public void Dispose()
        {
            _view.OnSaveClicked -= HandleSave;
            _view.OnLoadClicked -= HandleLoad;
        }

        private void HandleSave()
        {
            _saveSystem.Save();
            _view.ShowMessage("Игра сохранена!");
        }

        private void HandleLoad()
        {
            _saveSystem.Load();
            _view.ShowMessage("Игра загружена!");
        }
    }
}