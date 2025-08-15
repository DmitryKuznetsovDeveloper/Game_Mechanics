using System;
using UI.ViewModel;

namespace UI.System
{
    public sealed class SelectedPlayerProvider : ISelectedPlayerProvider
    {
        public event Action<IPlayerViewModel> OnChanged;
        public IPlayerViewModel Current { get; private set; }

        public void SetCurrent(IPlayerViewModel player)
        {
            if (ReferenceEquals(Current, player)) return;
            Current = player;
            OnChanged?.Invoke(Current);
        }
    }
}