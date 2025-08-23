using System.Collections.Generic;
using GameEngine.Providers;
using UnityEngine;
using GameEngine.Systems;
using SaveData;

namespace GameEngine.SaveModule
{
    public sealed class UnitsSaveModule : ISaveableModule
    {
        private readonly UnitManager _unitManager;
        private readonly IUnitPrefabProvider _prefabs;
        private readonly IGameSaveSystem _gameSaveSystem;

        public UnitsSaveModule(UnitManager unitManager, IUnitPrefabProvider prefabs, IGameSaveSystem gameSaveSystem)
        {
            _unitManager = unitManager;
            _prefabs = prefabs;
            _gameSaveSystem = gameSaveSystem;
            
            _gameSaveSystem.AddModule(this);
        }

        public void SaveInto(GameSaveData data)
        {
            Debug.Log("=====UnitsSaveModule::SaveInto Start=====");
            data.Units = new List<UnitData>();

            foreach (var unit in _unitManager.GetAllUnits())
            {
                if (unit == null)
                    continue;
                
                data.Units.Add(new UnitData
                {
                    Type = unit.Type,
                    HitPoints = unit.HitPoints,
                    Position = unit.transform.position,
                    Rotation = unit.transform.rotation.eulerAngles
                });
                Debug.Log($"Type ={unit.Type} / Health = {unit.HitPoints}");
            }
            Debug.Log("=====UnitsSaveModule::LoadFrom End=====");
        }

        public void LoadFrom(GameSaveData data)
        {
            Debug.Log("=====UnitsSaveModule::LoadFrom Start=====");
            _unitManager.DestroyAllUnit();
            
            foreach (var unit in data.Units)
            {
                var prefab = _prefabs.GetPrefabById(unit.Type);
                Debug.Log($"Type ={unit.Type} / Health = {unit.HitPoints}");
                
                if (prefab == null)
                {
                    continue;
                }

                var newUnit = _unitManager.SpawnUnit(
                    prefab,
                    unit.Position,
                    Quaternion.Euler(unit.Rotation));

                newUnit.HitPoints = unit.HitPoints;
            }
            Debug.Log("=====UnitsSaveModule::LoadFrom End=====");
        }
    }
}