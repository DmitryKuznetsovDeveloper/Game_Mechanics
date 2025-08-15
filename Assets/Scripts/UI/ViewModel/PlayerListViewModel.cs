using System;
using Core.Utilities;
using Features.Player.Configs;
using UI.System;
using UnityEngine;

namespace UI.ViewModel
{
    public sealed class PlayerListViewModel : IPlayerListViewModel
    {
        public event Action<IPlayerViewModel> OnPlayerSelected;
        public IPlayerViewModel CurrentPlayerSelected => _players[_currentIndex];
        
        private readonly ISelectedPlayerProvider _selectedPlayerProvider;
        private readonly IPlayerViewModel[] _players;
        private int _currentIndex;

        public PlayerListViewModel(ISelectedPlayerProvider selectedPlayerProvider)
        {
            _selectedPlayerProvider = selectedPlayerProvider;
            
            var playerViewModels = Resources.LoadAll<PlayerConfig>("Players");
            _players = new IPlayerViewModel[playerViewModels.Length];
            
            for (var i = 0; i < playerViewModels.Length; i++)
            {
                _players[i] = new PlayerViewModel(playerViewModels[i]);
            }
            
            _currentIndex = 0;
            
        }
        
        public void GoNext()
        {
            if (_players.Length == 0) 
                return;
            
            _currentIndex = (_currentIndex + 1) % _players.Length;
            OnPlayerSelected?.Invoke(_players[_currentIndex]);
        }

        public void GoPrev()
        {
            if (_players.Length == 0) 
                return;
            
            _currentIndex = (_currentIndex - 1 + _players.Length) % _players.Length;
            OnPlayerSelected?.Invoke(_players[_currentIndex]);
        }

        public void Select()
        {
            if (_players.Length == 0) 
                return;
            
            _selectedPlayerProvider.SetCurrent(_players[_currentIndex]);
            DebugUtil.Log($"Select {_players[_currentIndex].Name}");
        }
        
    }
}