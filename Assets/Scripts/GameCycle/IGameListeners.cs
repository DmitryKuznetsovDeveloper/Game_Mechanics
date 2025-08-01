namespace GameCycle
{
    public interface IGameListener
    {
    }

    public interface IGameStartListener : IGameListener
    {
        void OnStartGame();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnPauseGame();
    }

    public interface IGameResumeListener : IGameListener
    {
        void OnResumeGame();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnFinishGame();
    }
}