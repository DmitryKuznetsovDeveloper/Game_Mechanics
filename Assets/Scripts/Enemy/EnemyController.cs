using UnityEngine;
using Components;

namespace Enemy
{
    public sealed class EnemyController : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private float _fireCooldown = 2f;
        [SerializeField] private float _dwellTime = 6f;

        private GameObject _target;
        private Vector2 _destination;
        private float _fireTimer;
        private bool _reached;
        private float _dwellTimer;
        private EnemyPositions _positions;

        public void Initialize(Vector2 destination, GameObject target, EnemyPositions positions)
        {
            _destination = destination;
            _target = target;
            _positions = positions;
            _reached = false;
            _fireTimer = _fireCooldown;
            _dwellTimer = _dwellTime;
        }

        private void FixedUpdate()
        {
            if (_reached) TryAttack();
            else TryMove();
        }

        private void TryMove()
        {
            Vector2 current = transform.position;
            Vector2 delta = _destination - current;

            if (delta.sqrMagnitude < 0.25f * 0.25f)
            {
                _reached = true;
                _moveComponent.MoveByRigidbodyVelocity(Vector2.zero);
            }
            else
            {
                _moveComponent.MoveByRigidbodyVelocity(delta.normalized);
            }
        }

        private void TryAttack()
        {
            if (!_target ||
                !_target.TryGetComponent<HitPointsComponent>(out var hp) ||
                !hp.HasHitPoints())
                return;

            _fireTimer -= Time.fixedDeltaTime;
            if (_fireTimer <= 0f)
            {
                _attackComponent.FireAtTarget(_target.transform.position);
                _fireTimer = _fireCooldown;
            }

            _dwellTimer -= Time.fixedDeltaTime;
            if (_dwellTimer <= 0f)
            {
                _positions.ReleaseAttackPosition(_destination);
                _destination = _positions.RandomAttackPosition();
                _reached = false;
                _dwellTimer = _dwellTime;
                _fireTimer = _fireCooldown;
            }
        }
    }
}