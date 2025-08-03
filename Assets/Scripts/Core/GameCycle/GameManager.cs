using System;
using System.Collections.Generic;
using Core.Utilities;

namespace Core.GameCycle
{
    public sealed class GameManager
    {
        public event Action OnGameStarted;
        public event Action OnGamePaused;
        public event Action OnGameResumed;
        public event Action OnGameFinished;
        
        public GameState State { get; private set; }

        private readonly List<IGameListener> _listeners = new();

        public void AddListener(IGameListener listener)
        {
            _listeners.Add(listener);
        }

        public void RemoveListener(IGameListener listener)
        {
            _listeners.Remove(listener);
        }

        public void StartGame()
        {
            if (State != GameState.OFF)
            {
                return;
            }

            foreach (var it in _listeners)
            {
                if (it is IGameStartListener listener)
                {
                    listener.OnStartGame();
                }
            }
            
            State = GameState.PLAY;
            OnGameStarted?.Invoke();
            DebugUtil.Log("Game Started");
        }

        public void PauseGame()
        {
            if (State != GameState.PLAY)
            {
                return;
            }
            
            foreach (var it in _listeners)
            {
                if (it is IGamePauseListener listener)
                {
                    listener.OnPauseGame();
                }
            }
            
            State = GameState.PAUSE;
            OnGamePaused?.Invoke();
            DebugUtil.Log("Game Paused");
        }
        
        public void ResumeGame()
        {
            if (State != GameState.PAUSE)
            {
                return;
            }
            
            foreach (var it in _listeners)
            {
                if (it is IGameResumeListener listener)
                {
                    listener.OnResumeGame();
                }
            }
            
            State = GameState.PLAY;
            OnGameResumed?.Invoke();
            DebugUtil.Log("Game Resumed");
        }
        
        public void FinishGame()
        {
            if (State is not (GameState.PAUSE or GameState.PLAY))
            {
                return;
            }
            
            foreach (var it in _listeners)
            {
                if (it is IGameFinishListener listener)
                {
                    listener.OnFinishGame();
                }
            }
            
            State = GameState.FINISH;
            OnGameFinished?.Invoke();
            DebugUtil.Log("Game Finished");
        }
    }
}