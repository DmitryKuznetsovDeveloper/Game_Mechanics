﻿namespace Core
{
    public interface IGameListener
    {
    }

    public interface IGameStartListener : IGameListener
    {
        void OnStartGame();
    }
    
    public interface IGamePostStartListener : IGameListener
    {
        void OnPostStartGame();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPause();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResume();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnFinishGame();
    }

    public interface IGameUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }

    public interface IGameFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }

    public interface IGameLateUpdateListener : IGameListener
    {
        void OnLateUpdate(float deltaTime);
    }
}