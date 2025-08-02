using Bullets;
using Character;
using Components;
using Input;
using Systems;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class CharacterInstaller : MonoInstaller
    {
        [SerializeField] private int _minMovePositionX;
        [SerializeField] private int _maxMovePositionX;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private float _speed;
        [SerializeField] private bool _isPlayer;
        [SerializeField] private int _maxHitPoints = 5;
        [SerializeField] private Transform _firePoint;
        [SerializeField] private BulletConfig _bulletConfig;

        public override void InstallBindings()
        {
            //View
            Container.Bind<CharacterView>().FromComponentInHierarchy().AsSingle();

            //Main
            Container.BindInterfacesAndSelfTo<InputReader>().AsSingle();
            Container.BindInterfacesTo<CharacterInputController>().AsSingle()
                .WithArguments(_minMovePositionX, _maxMovePositionX);
            Container.BindInterfacesAndSelfTo<MoveComponent>().FromNew().AsSingle().WithArguments(_speed, _rigidbody);

            //Weapon
            Container.Bind<WeaponComponent>().FromNew().AsSingle().WithArguments(_firePoint);
            Container.Bind<TeamComponent>().FromNew().AsSingle().WithArguments(_isPlayer);
            Container.Bind<AttackComponent>().FromNew().AsSingle().WithArguments(_bulletConfig);
            Container.Bind<AttackSystem>().AsSingle();

            //Health
            Container.Bind<HitPointsComponent>().FromNew().AsSingle().WithArguments(_maxHitPoints);
            Container.BindInterfacesTo<CharacterDeathHandler>().FromNew().AsSingle();
        }
    }
}