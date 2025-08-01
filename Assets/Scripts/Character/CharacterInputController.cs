using GameCycle;
using Input;
using Systems;

namespace Character
{
    public sealed class CharacterInputController : IGameStartListener, IGameFixedTickable, IGameFinishListener
    {
        private readonly AttackSystem _attackSystem;
        private readonly MoveComponent _moveComponent;
        private readonly InputReader _inputReader;

        public CharacterInputController(
            AttackSystem attackSystem,
            MoveComponent moveComponent,
            InputReader inputReader)
        {
            _attackSystem = attackSystem;
            _moveComponent = moveComponent;
            _inputReader = inputReader;
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
            _moveComponent.MoveByRigidbodyVelocity(_inputReader.MoveDirection);
        }

        private void HandleFire()
        {
            _attackSystem.Fire();
        }
    }
}