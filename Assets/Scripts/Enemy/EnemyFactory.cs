using Character;
using Core;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyFactory : IGameStartListener
    {
        private readonly Transform _world;
        
        private CharacterView _player;
        private EnemyPositions _positions;


        public EnemyFactory(Transform world)
        {
            _world = world;
        }
        
        public void OnStartGame()
        {
            _player = ServiceLocator.Resolve<CharacterView>();
            _positions = ServiceLocator.Resolve<EnemyPositions>();
        }

        public void SetupEnemy(EnemyView view)
        {
            var spawnPos = _positions.RandomSpawnPosition().position;
            var attackPos = _positions.RandomAttackPosition();
            
            var enemy = view.Root;
            enemy.transform.SetParent(_world);
            enemy.transform.position = spawnPos;

            if (enemy.TryGetComponent<EnemyController>(out var controller))
            {
                controller.Initialize(attackPos, _player.Root, _positions);
            }
        }
    }
}