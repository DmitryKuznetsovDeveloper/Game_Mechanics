using GameCycle;
using Input;
using Systems;
using Utils;

namespace Player
{
    public sealed class PlayerInputController : IGameStartListener, IGameFixedTickable, IGameFinishListener
    {
        private readonly InputReader _inputReader;
        private readonly PlayerFacade _playerFacade;
        private readonly AttackSystem _attackSystem;
        private readonly PlayerConfig _playerConfig;

        public PlayerInputController(
            InputReader inputReader, 
            PlayerFacade playerFacade,
            AttackSystem attackSystem,
            PlayerConfig playerConfig)
        {
            _inputReader = inputReader;
            _playerFacade = playerFacade;
            _attackSystem = attackSystem;
            _playerConfig = playerConfig;
        }


        public void OnStartGame()
        {
            _inputReader.OnFirePressed += HandleFire;
        }

        public void OnFinishGame()
        {
            _inputReader.OnFirePressed -= HandleFire;
        }

        public void FixedTick(float deltaTime)
        {
            _playerFacade.MoveByRigidbodyVelocityClamped(_inputReader.MoveDirection, _playerConfig.MinMovePositionX,
                _playerConfig.MaxMovePositionX);
        }

        private void HandleFire()
        {
            _attackSystem.Fire(_playerFacade.AttackComponent);
        }
    }
}