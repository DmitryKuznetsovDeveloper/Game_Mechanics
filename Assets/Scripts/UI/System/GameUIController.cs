using System;
using System.Threading;
using Core.GameCycle;
using Core.Shared.Components;
using Cysharp.Threading.Tasks;
using UI.View;
using Zenject;

namespace UI.System
{
    public class GameUIController : IInitializable, IDisposable, IGameStartListener, IGameFinishListener
    {
        private readonly GameUIView _view;
        private readonly GameManager _gameManager;
        private readonly TimerComponent _timer;
        readonly int _countdownTime;

        CancellationTokenSource _cts;

        [Inject]
        public GameUIController(
            GameUIView view,
            TimerComponent timer,
            GameManager gameManager,
            [Inject(Id = "UI_CountdownTime")] int countdownTime)
        {
            _view = view;
            _timer = timer;
            _gameManager = gameManager;
            _countdownTime = countdownTime;
        }

        // Автоматически вызывается Zenject'ом после биндинга
        public void Initialize()
        {
            _view.OnStartClicked += HandleStart;
            _view.OnPauseClicked += HandlePause;
            _view.OnResumeClicked += HandleResume;
            _view.ShowStartButton();
        }

        public void Dispose()
        {
            _view.OnStartClicked -= HandleStart;
            _view.OnPauseClicked -= HandlePause;
            _view.OnResumeClicked -= HandleResume;
            _cts?.Cancel();
        }

        // IGameStartListener
        public void OnStartGame()
        {
            // после старта игры кнопка «Pause»
            _view.ShowPauseButton();
        }

        // IGameFinishListener
        public void OnFinishGame()
        {
            // при завершении перезагрузим сцену
            _cts?.Cancel();
            // …здесь логика перезагрузки
        }

        private void HandleStart()
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            
            _ = PlayCountdownAsync(_countdownTime,_cts.Token);
        }


        private async UniTask PlayCountdownAsync(int seconds, CancellationToken token = default)
        {
            _view.ShowCountdown();

            await _timer.CountdownAsync(seconds, remaining =>
            {
                var text = remaining > 0 ? remaining.ToString() : "GO";
                _view.SetCountdownText(text);
            }, token);

            _view.HideCountdown();
            _gameManager.StartGame();
        }

        private void HandlePause()
        {
            _gameManager.PauseGame();
            _view.ShowResumeButton();
        }

        private void HandleResume()
        {
            _gameManager.ResumeGame();
            _view.ShowPauseButton();
        }
    }
}