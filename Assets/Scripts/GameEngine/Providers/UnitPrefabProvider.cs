using System.Collections.Generic;
using System.Linq;
using GameEngine.Objects;
using UnityEngine;

namespace GameEngine.Providers
{
    public interface IUnitPrefabProvider
    {
        Unit GetPrefabById(string id);
    }
    
    public sealed class UnitPrefabProvider : IUnitPrefabProvider
    {
        private const string UNITS_PREFAB_PATH = "Units/";
        private readonly Dictionary<string, Unit> _units;

        public UnitPrefabProvider()
        {
            _units = Resources.LoadAll<Unit>(UNITS_PREFAB_PATH)
                .ToDictionary(u => u.Type, u => u);
        }

        public Unit GetPrefabById(string id)
        {
            if (!_units.TryGetValue(id, out var prefab))
            {
                Debug.LogError($"[UnitPrefabProvider] Префаб с Id '{id}' не найден!");
                return null;
            }

            return prefab;

        }
    }
}