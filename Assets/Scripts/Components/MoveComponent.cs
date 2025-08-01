using GameCycle;
using UnityEngine;

public sealed class MoveComponent : IGamePauseListener, IGameResumeListener, IGameFinishListener
{
    private float _speed;
    private Vector2 _storedVelocity;
    private readonly Rigidbody2D _rb;
    private RigidbodyConstraints2D _originalConstraints;

    public MoveComponent(float speed, Rigidbody2D rb)
    {
        _speed = speed;
        _rb = rb;
        _originalConstraints = _rb.constraints;
    }

    public void SetSpeed(float speed) => _speed = speed;

    public void MoveByRigidbodyVelocity(Vector2 direction)
    {
        _rb.linearVelocity = direction * _speed;
    }

    public void OnPauseGame()
    {
        _storedVelocity = _rb.linearVelocity;
        Freeze();
    }

    public void OnResumeGame()
    {
        Unfreeze();
        _rb.linearVelocity = _storedVelocity;
    }

    public void OnFinishGame()
    {
        Freeze();
    }

    public void Freeze()
    {
        _originalConstraints = _rb.constraints;
        _rb.linearVelocity = Vector2.zero;
        _rb.angularVelocity = 0f;
        _rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    public void Unfreeze()
    {
        _rb.constraints = _originalConstraints;
    }
}
