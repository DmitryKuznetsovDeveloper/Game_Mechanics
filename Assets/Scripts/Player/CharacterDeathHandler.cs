using Components;
using GameCycle;

namespace Player
{
    public sealed class CharacterDeathHandler : IGameStartListener, IGameFinishListener
    {
        private readonly HitPointsComponent _hitPointsComponent;
        private readonly GameManager _gameManager;

        public CharacterDeathHandler(HitPointsComponent hitPointsComponent)
        {
            _hitPointsComponent = hitPointsComponent;
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