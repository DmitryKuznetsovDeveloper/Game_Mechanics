using UnityEngine;

namespace Character
{
    public class CharacterSpawnSystem

    {
        private readonly CharacterView _playerPrefab;
        private readonly Transform _spawnPoint;

        public CharacterSpawnSystem(CharacterView playerPrefab, Transform spawnPoint)
        {
            _playerPrefab = playerPrefab;
            _spawnPoint = spawnPoint;
        }

        public CharacterView SpawnPlayer()
        {
            return Object.Instantiate(_playerPrefab.gameObject, _spawnPoint.position, _spawnPoint.rotation).GetComponent<CharacterView>();
        }
    }
}