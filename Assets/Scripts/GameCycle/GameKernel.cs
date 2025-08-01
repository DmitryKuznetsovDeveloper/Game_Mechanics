using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace GameCycle
{
    public sealed class GameKernel : MonoKernel,
        IGameStartListener, 
        IGamePauseListener, 
        IGameResumeListener,
        IGameFinishListener
    {
        [Inject]
        private GameManager _gameManager;
        
        [InjectLocal]
        private List<IGameListener> _listeners = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameTickable> _tickables = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameFixedTickable> _fixedTickables = new();

        [Inject(Optional = true, Source = InjectSources.Local)]
        private List<IGameLateTickable> _lateTickables = new();

        public override void Start()
        {
            base.Start();
            _gameManager.AddListener(this);
        }
        
        public override void Update()
        {
            base.Update();

            if (_gameManager.State != GameState.PLAY) 
                return;
            
            var deltaTime = Time.deltaTime;
            foreach (var tickable in _tickables)
            {
                tickable.Tick(deltaTime);
            }
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_gameManager.State != GameState.PLAY) 
                return;
            
            var deltaTime = Time.fixedDeltaTime;
            foreach (var tickable in _fixedTickables)
            {
                tickable.FixedTick(deltaTime);
            }
        }

        public override void LateUpdate()
        {
            base.LateUpdate();

            if (_gameManager.State != GameState.PLAY) 
                return;
            
            var deltaTime = Time.deltaTime;
            foreach (var tickable in _lateTickables)
            {
                tickable.LateTick(deltaTime);
            }
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            _gameManager.RemoveListener(this);
        }

        void IGameStartListener.OnStartGame()
        {
            foreach (var it in _listeners)
            {
                if (it is IGameStartListener listener)
                {
                    listener.OnStartGame();
                }
            }
        }

        void IGamePauseListener.OnPauseGame()
        {
            foreach (var it in _listeners)
            {
                if (it is IGamePauseListener listener)
                {
                    listener.OnPauseGame();
                }
            }
        }

        void IGameResumeListener.OnResumeGame()
        {
            foreach (var it in _listeners)
            {
                if (it is IGameResumeListener listener)
                {
                    listener.OnResumeGame();
                }
            }
        }

        void IGameFinishListener.OnFinishGame()
        {
            foreach (var it in _listeners)
            {
                if (it is IGameFinishListener listener)
                {
                    listener.OnFinishGame();
                }
            }
        }
    }
}