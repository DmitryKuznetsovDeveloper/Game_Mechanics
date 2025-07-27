using UnityEngine;
using Components;

namespace Enemy
{
    public sealed class EnemyController : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private AttackComponent _attackComponent;
        [SerializeField] private float _fireCooldown = 2f;

        private GameObject _target;
        private Vector2 _destination;
        private float _fireTimer;
        private bool _reached;

        public void Initialize(Vector2 destination, GameObject target)
        {
            _destination = destination;
            _target = target;
            _reached = false;
            _fireTimer = _fireCooldown;
        }

        private void FixedUpdate()
        {
            if (_reached) TryAttack();
            else TryMove();
        }

        private void TryMove()
        {
            var delta = _destination - (Vector2)transform.position;
            if (delta.magnitude < 0.25f)
            {
                _reached = true;
                _moveComponent.MoveByRigidbodyVelocity(Vector2.zero);
            }
            else
            {
                var dir = delta.normalized;
                _moveComponent.MoveByRigidbodyVelocity(dir);
            }
        }

        private void TryAttack()
        {
            if (!_target || !_target.TryGetComponent(out HitPointsComponent hp) || !hp.HasHitPoints())
                return;

            _fireTimer -= Time.fixedDeltaTime;
            if (!(_fireTimer <= 0f)) 
                return;
            
            _attackComponent.FireAtTarget(_target.transform.position);
            _fireTimer = _fireCooldown;
        }
    }
}