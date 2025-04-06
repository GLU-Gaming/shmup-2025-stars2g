using UnityEngine;
using System.Collections.Generic;

public enum WaveType
{
    WaitForEnemies,
    WaitForTime
}

public enum EndType
{
    finishline,
    boss
}

[CreateAssetMenu(menuName = "Levels/Waves")]
public class AdvancedWave : ScriptableObject
{
    public List<Wave> waveCollection;
    [System.Serializable]
    public class Wave
    {
        public List<Enemy> enemies;
        public WaveType waveType;
        public float waveDelay;
    }

    [System.Serializable]
    public class Enemy
    {
        [Range(0, 3)] public int Type;
        public EntryType entryType;
        public float SpawnDelay;
    }
}
