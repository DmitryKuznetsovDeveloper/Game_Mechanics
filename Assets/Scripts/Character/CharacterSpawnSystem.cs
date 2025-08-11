using UnityEngine;

namespace Character
{
    public class CharacterSpawnSystem

    {
        private readonly GameObject _playerPrefab;
        private readonly Transform _spawnPoint;

        public CharacterSpawnSystem(GameObject playerPrefab, Transform spawnPoint)
        {
            _playerPrefab = playerPrefab;
            _spawnPoint = spawnPoint;
        }

        public GameObject SpawnPlayer()
        {
            return Object.Instantiate(_playerPrefab, _spawnPoint.position, _spawnPoint.rotation);
        }
    }
}