using SaveData;

namespace GameEngine.SaveModule
{
    public interface ISaveableModule
    {
        void SaveInto(GameSaveData data);
        void LoadFrom(GameSaveData data);
    }
}