using System.Collections.Generic;
using GameEngine.SaveModule;
using SaveData;
using SaveSystem;
using Zenject;

namespace GameEngine.Systems
{
    
    public interface IGameSaveSystem
    {
        void Save();
        void Load();
        void AddModule(ISaveableModule module);
        void RemoveModule(ISaveableModule module);
    }
    
    public sealed class GameSaveSystem : IGameSaveSystem
    {
        private readonly ISaveLoadRepository<GameSaveData> _repository;
        private readonly List<ISaveableModule> _modules = new ();

        [Inject]
        public GameSaveSystem(ISaveLoadRepository<GameSaveData> repository)
        {
            _repository = repository;
        }

        public void AddModule(ISaveableModule module)
        {
            _modules.Add(module);
        }
        
        public void RemoveModule(ISaveableModule module)
        {
            _modules.Remove(module);
        }

        public void Save()
        {
            var saveData = new GameSaveData();
            
            foreach (var module in _modules)
            {
                module.SaveInto(saveData);
            }
            _repository.Save(saveData);
        }

        public void Load()
        {
            var saveData = _repository.Load();
            
            if (saveData == null)
            {
                return;
            }

            foreach (var module in _modules)
            {
                module.LoadFrom(saveData);
            }
        }
    }
}