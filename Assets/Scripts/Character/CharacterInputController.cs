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
        private readonly int _minX;
        private readonly int _maxX;

        public CharacterInputController(
            AttackSystem attackSystem,
            MoveComponent moveComponent,
            InputReader inputReader, 
            int minX, 
            int maxX)
        {
            _attackSystem = attackSystem;
            _moveComponent = moveComponent;
            _inputReader = inputReader;
            _minX = minX;
            _maxX = maxX;
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
            _moveComponent.MoveByRigidbodyVelocityClamped(_inputReader.MoveDirection,_minX,_maxX);
        }

        private void HandleFire()
        {
            _attackSystem.Fire();
        }
    }
}