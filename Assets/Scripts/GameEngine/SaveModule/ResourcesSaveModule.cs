using System.Collections.Generic;
using GameEngine.Systems;
using SaveData;
using UnityEngine;

namespace GameEngine.SaveModule
{
    public sealed class ResourcesSaveModule : ISaveableModule
    {
        private readonly ResourceService _resourceService;
        private readonly IGameSaveSystem gameSaveSystem;

        public ResourcesSaveModule(ResourceService resourceService, IGameSaveSystem gameSaveSystem)
        {
            _resourceService = resourceService;
            this.gameSaveSystem = gameSaveSystem;

            this.gameSaveSystem.AddModule(this);
        }

        public void SaveInto(GameSaveData data)
        {
            Debug.Log("=====ResourcesSaveModule::SaveInto Start=====");
            data.Resources = new List<ResourceData>();

            foreach (var resource in _resourceService.GetResources())
            {
                data.Resources.Add(new ResourceData
                {
                    Id = resource.ID,
                    Amount = resource.Amount
                });
                Debug.Log($"Saved resource {resource.ID} to {resource.Amount}");
            }
            Debug.Log("=====ResourcesSaveModule::SaveInto End=====");
        }

        public void LoadFrom(GameSaveData data)
        {
            Debug.Log("=====ResourcesLoadModule::LoadFrom Start=====");
            foreach (var resource in _resourceService.GetResources())
            {
                var saved = data.Resources.Find(r => r.Id == resource.ID);
                Debug.Log($"Saved resource {saved.Id} to {saved.Amount}");
                if (saved != null)
                {
                    resource.Amount = saved.Amount;
                }
            }
            Debug.Log("=====ResourcesLoadModule::LoadFrom End=====");
        }
    }
}