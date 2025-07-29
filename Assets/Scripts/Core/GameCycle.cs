using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public sealed class GameCycle : MonoBehaviour
    {
        private List<IGameListener> _gameListeners = new();
        private List<IGameUpdateListener> _gameUpdateListeners = new();
        private List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();
        private List<IGameLateUpdateListener> _gameLateUpdateListeners = new();

        private GameState _currentState = GameState.NONE;

        private void Update()
        {
            if (_currentState != GameState.PLAYING)
                return;
            
            var deltaTime = Time.deltaTime;
            
            foreach (var listener in _gameUpdateListeners)
                listener.OnUpdate(deltaTime);
        }

        private void FixedUpdate()
        {
            if (_currentState != GameState.PLAYING)
                return;

            var fixDeltaTime = Time.fixedDeltaTime;
            
            foreach (var listener in _gameFixedUpdateListeners)
                listener.OnFixedUpdate(fixDeltaTime);
        }

        private void LateUpdate()
        {
            if (_currentState != GameState.PLAYING)
                return;

            var deltaTime = Time.deltaTime;
            
            foreach (var listener in _gameLateUpdateListeners)
                listener.OnLateUpdate(deltaTime);
        }

        public void AddListener(IGameListener listener)
        {
            _gameListeners.Add(listener);

            if (listener is IGameUpdateListener gameUpdateListener)
                _gameUpdateListeners.Add(gameUpdateListener);

            if (listener is IGameFixedUpdateListener gameFixedUpdateListener)
                _gameFixedUpdateListeners.Add(gameFixedUpdateListener);

            if (listener is IGameLateUpdateListener gameLateUpdateListener)
                _gameLateUpdateListeners.Add(gameLateUpdateListener);
        }

        public void StartGame()
        {
            if (_currentState != GameState.NONE && _currentState != GameState.FINISHED)
                return;

            _currentState = GameState.STARTING;

            foreach (var listener in _gameListeners)
            {
                if (listener is IGameStartListener startListener)
                    startListener.OnStartGame();
            }
            
            foreach (var listener in _gameListeners)
            {
                if (listener is IGamePostStartListener postListener)
                    postListener.OnPostStartGame();
            }
            
            _currentState = GameState.PLAYING;
        }

        public void PauseGame()
        {
            if (_currentState != GameState.PLAYING)
                return;

            _currentState = GameState.PAUSED;

            foreach (var listener in _gameListeners)
            {
                if (listener is IGamePauseListener pauseListener)
                    pauseListener.OnPause();
            }
        }

        public void ResumeGame()
        {
            if (_currentState != GameState.PAUSED)
                return;

            _currentState = GameState.PLAYING;

            foreach (var listener in _gameListeners)
            {
                if (listener is IGameResumeListener resumeListener)
                    resumeListener.OnResume();
            }
        }

        public void FinishGame()
        {
            if (_currentState == GameState.FINISHED)
                return;

            _currentState = GameState.FINISHED;

            foreach (var listener in _gameListeners)
            {
                if (listener is IGameFinishListener finishListener)
                    finishListener.OnFinishGame();
            }
        }

        private enum GameState
        {
            NONE,
            STARTING,
            PLAYING,
            PAUSED,
            FINISHED
        }
    }
}