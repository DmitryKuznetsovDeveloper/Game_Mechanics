using System;
using System.Collections.Generic;
using UnityEngine;

namespace SaveData
{
    [Serializable]
    public class GameSaveData
    {
        public List<UnitData> Units;
        public List<ResourceData> Resources;
    }

    [Serializable]
    public class UnitData
    {
        public string Type;
        public int HitPoints;
        public Vector3 Position;
        public Vector3 Rotation;
    }

    [Serializable]
    public class ResourceData
    {
        public string Id;
        public int Amount;
    }
}