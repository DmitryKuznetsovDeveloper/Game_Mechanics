using System;
using UI.ViewModel;

namespace UI.System
{
    public interface ISelectedPlayerProvider
    {
        event Action<IPlayerViewModel> OnChanged;
        IPlayerViewModel Current { get; }
        void SetCurrent(IPlayerViewModel player);
    }
}