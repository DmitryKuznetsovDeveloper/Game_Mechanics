using System;
using UnityEngine;

namespace UI.ViewModel
{
    public interface IPlayerListViewModel
    {
        event Action<IPlayerViewModel> OnPlayerSelected;

        IPlayerViewModel CurrentPlayerSelected { get; }

        void GoNext();
        void GoPrev();
        void Select();
    }

    public interface IPlayerViewModel
    {
        string Name { get; }
        int HitPoints { get; }
        int Damage { get; }
        float Speed { get; }
    }
}