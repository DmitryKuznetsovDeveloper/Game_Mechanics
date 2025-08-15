using Core.GameCycle;
using Core.Shared.Components;
using Core.Utilities;
using UnityEngine.SceneManagement;

namespace Features.Player.System
{
    public sealed class PlayerDeathHandler : IGameStartListener, IGameFinishListener
    {
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly GameManager _gameManager;

        public PlayerDeathHandler(HitPointsComponent hitPointsComponent, GameManager gameManager)
        {
            _hitPointsComponent = hitPointsComponent;
            _gameManager = gameManager;
        }

        public void OnStartGame()
        {
            _hitPointsComponent.OnDeath += OnCharacterDeath;
        }

        public void OnFinishGame()
        {
            _hitPointsComponent.OnDeath -= OnCharacterDeath;
        }

        private void OnCharacterDeath()
        {
            DebugUtil.Log($"PlayerDeathHandler::OnCharacterDeath");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _gameManager.FinishGame();
        }
    }
}