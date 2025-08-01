using GameCycle;
using UnityEngine;
using Zenject;

namespace Core
{
    public sealed class GameController : ITickable
    {
        private readonly GameManager _gameManager;

        public GameController(GameManager gameManager)
        {
            _gameManager = gameManager;
        }

        void ITickable.Tick()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.I))
            {
                _gameManager.StartGame();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.P))
            {
                _gameManager.PauseGame();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.R))
            {
                _gameManager.ResumeGame();
            }

            if (UnityEngine.Input.GetKeyDown(KeyCode.F))
            {
                _gameManager.FinishGame();
            }
        }
    }
}