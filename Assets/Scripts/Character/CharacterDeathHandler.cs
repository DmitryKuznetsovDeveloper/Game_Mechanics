using Components;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(HitPointsComponent))]
    public sealed class CharacterDeathHandler : MonoBehaviour
    {
        [SerializeField] GameManager.GameManager _gameManager;
        private HitPointsComponent _hitPointsComponent;

        private void Awake()
        {
            _hitPointsComponent = GetComponent<HitPointsComponent>();
        }

        private void OnEnable()
        {
            _hitPointsComponent.OnDeath += OnCharacterDeath;
        }

        private void OnDisable()
        {
            _hitPointsComponent.OnDeath -= OnCharacterDeath;
        }
        
        private void OnCharacterDeath(GameObject _)
        {
            _gameManager.FinishGame();
        }
    }
}