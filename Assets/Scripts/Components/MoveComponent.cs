using Core;
using UnityEngine;

namespace Components
{
    
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class MoveComponent : MonoBehaviour, IGamePauseListener,IGameResumeListener, IGameFinishListener
    {
        [SerializeField] private float _speed = 5.0f;
        
        private Rigidbody2D _rb;
        private Vector2 _storedVelocity;

        private void Awake()
        {
            ServiceLocator.Resolve<GameCycle>().AddListener(this);
            
            _rb = GetComponent<Rigidbody2D>();
        }
        
        public void SetSpeed(float speed)
        {
            _speed = speed;
        }
        
        public void MoveByRigidbodyVelocity(Vector2 direction)
        {
            _rb.linearVelocity = direction * _speed;
        }

        public void OnPause()
        {
            _storedVelocity = _rb.linearVelocity;
            StopMove();
        }
        
        public void OnResume()
        {
            _rb.linearVelocity = _storedVelocity;
        }

        public void OnFinishGame()
        {
            _rb.linearVelocity = Vector2.zero;
        }
        
        private void StopMove()
        {
            _rb.linearVelocity = Vector2.zero;
        }
    }
}