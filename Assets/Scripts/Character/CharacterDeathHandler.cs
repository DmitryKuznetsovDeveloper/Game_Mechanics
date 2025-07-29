using Components;
using Core;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class CharacterDeathHandler : MonoBehaviour,IGameStartListener, IGameFinishListener
    {
        private HitPointsComponent _hitPointsComponent;
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = ServiceLocator.Resolve<GameManager>();
            ServiceLocator.Resolve<GameCycle>().AddListener(this);
            
            _hitPointsComponent = GetComponent<HitPointsComponent>();
        }

        public void OnStartGame()
        {
            _hitPointsComponent.OnDeath += OnCharacterDeath;
        }

        public void OnFinishGame()
        {
            _hitPointsComponent.OnDeath -= OnCharacterDeath;
        }
        
        private void OnCharacterDeath(GameObject _)
        {
            _gameManager.FinishGame();
        }
    }
}