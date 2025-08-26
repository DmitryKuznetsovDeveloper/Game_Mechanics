using GameEngine.Systems;
using SaveData;
using SaveSystem;
using UnityEngine;
using Zenject;

namespace GameEngine.Installers
{
    [CreateAssetMenu(fileName = "GlobalInstaller", menuName = "Installers/GlobalInstaller")]
    public sealed class GlobalInstaller : ScriptableObjectInstaller<GlobalInstaller>
    {
        private const string SAVE_GAME = "save.dat"; 
        private const string ENCRYPTION_KEY = "DontHackMeBro42";

        public override void InstallBindings()
        {
            var path = System.IO.Path.Combine(Application.persistentDataPath, SAVE_GAME);
            
            Container.Bind<ISaveLoadRepository<GameSaveData>>()
                .To<EncryptedJsonFileStorage<GameSaveData>>()
                .AsSingle()
                .WithArguments(path, ENCRYPTION_KEY);

            Container.Bind<IGameSaveSystem>()
                .To<GameSaveSystem>()
                .AsSingle()
                .NonLazy();
        }
    }
}