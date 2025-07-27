using Bullets;
using Character;
using UnityEngine;
using Components;
using Enemy;
using Input;

namespace GameManager
{
    public sealed class GameManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject _player;
        [SerializeField] private InputReader _inputReader;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private EnemySystem _enemySystem;

        private void Awake()
        {
            InjectDependencies();
        }
        
        public void FinishGame()
        {
            Debug.Log("Game over!");
            Time.timeScale = 0;
        }

        private void InjectDependencies()
        {
            _player.GetComponent<PlayerInputController>().InjectDependencies(_bulletSystem,_inputReader);
            _player.GetComponent<CharacterDeathHandler>().InjectDependencies(this);
            _enemySystem.InjectDependencies(_bulletSystem,_player.gameObject);
        }
    }
}