using UnityEngine;

namespace ShootEmUp
{
    public sealed class DeathHandler : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private GameManager _gameManager;

        private void OnEnable()
        {
            _hitPointsComponent.OnHpEmpty += OnCharacterDeath;
        }

        private void OnDisable()
        {
            _hitPointsComponent.OnHpEmpty -= OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject _)
        {
            _gameManager.FinishGame();
        }
    }
}