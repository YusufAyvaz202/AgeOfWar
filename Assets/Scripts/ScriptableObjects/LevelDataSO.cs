using System.Collections.Generic;
using Misc;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "NewLevel", menuName = "ScriptableObject/Level", order = 0)]
    public class LevelDataSO : ScriptableObject
    {
       public int LevelId;
       public float TimeBetweenEachFighter;
       public List<Wave> Waves = new();
    }

    [System.Serializable]
    public class Wave
    {
       public List<Unit> Units = new();
       public float SpawnDelayBetweenWave;
    }

    [System.Serializable]
    public class Unit
    {
        public FighterType FighterType;
        public int UnitSpawnCount;
        public float TimeBetweenEachUnits;
    }
}