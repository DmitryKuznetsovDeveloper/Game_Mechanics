using Bullets;
using Components;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyFactory
    {
        private readonly Transform _world;
        private readonly GameObject _target;
        private readonly EnemySystem _enemySystem;
        private readonly BulletSystem _bulletSystem;

        public EnemyFactory(Transform world, GameObject target, BulletSystem bulletSystem, EnemySystem enemySystem)
        {
            _world = world;
            _target = target;
            _bulletSystem = bulletSystem;
            _enemySystem = enemySystem;
        }

        public void SetupEnemy(EnemyView view, Vector2 spawnPos, Vector2 destination)
        {
            var enemy = view.Root;
            enemy.transform.SetParent(_world);
            enemy.transform.position = spawnPos;

            if (enemy.TryGetComponent(out AttackComponent attack))
                attack.InjectDependencies(_bulletSystem);

            if (enemy.TryGetComponent(out EnemyDeathHandler deathHandler))
                deathHandler.InjectDependencies(_enemySystem);
            
            if (enemy.TryGetComponent(out EnemyController controller))
                controller.Initialize(destination, _target);
        }
    }
}