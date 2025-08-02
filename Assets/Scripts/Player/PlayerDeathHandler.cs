using Components;
using GameCycle;

namespace Player
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
            _gameManager.FinishGame();
        }
    }
}