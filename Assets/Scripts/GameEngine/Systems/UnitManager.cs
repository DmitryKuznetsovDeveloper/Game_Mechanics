using System;
using System.Collections.Generic;
using System.Linq;
using GameEngine.Objects;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameEngine.Systems
{
    //Нельзя менять!
    [Serializable]
    public sealed class UnitManager
    {
        [SerializeField] private Transform container;

        [ShowInInspector] [ReadOnly] private HashSet<Unit> sceneUnits = new();

        public UnitManager()
        {
        }

        public UnitManager(Transform container)
        {
            this.container = container;
        }

        public void SetupUnits(IEnumerable<Unit> units)
        {
            sceneUnits = new HashSet<Unit>(units);
        }

        public void SetContainer(Transform container)
        {
            this.container = container;
        }

        [Button]
        public Unit SpawnUnit(Unit prefab, Vector3 position, Quaternion rotation)
        {
            var unit = Object.Instantiate(prefab, position, rotation, container);
            sceneUnits.Add(unit);
            return unit;
        }

        [Button]
        public void DestroyUnit(Unit unit)
        {
            if (sceneUnits.Remove(unit))
            {
                Object.Destroy(unit.gameObject);
            }
        }

        public void DestroyAllUnit()
        {
            if (sceneUnits == null)
            {
                return;
            }
            
            foreach (var unit in sceneUnits.ToList())
            {
                if (unit == null)
                    return;
                
                Object.Destroy(unit.gameObject);
                sceneUnits.Remove(unit);
            }
        }
        
        public IEnumerable<Unit> GetAllUnits()
        {
            return sceneUnits;
        }
    }
}