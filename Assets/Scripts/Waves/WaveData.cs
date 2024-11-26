using System.Collections.Generic;
using slaughter.de.Actors.Enemy;
using UnityEngine;

namespace slaughter.de.Waves
{
    [CreateAssetMenu(menuName = "Data/WaveData", fileName = "WaveData")]
    public class WaveData : ScriptableObject
    {
        public string waveName;

        public List<EnemyData> enemyData = new();

        public float waveDuration;
        public float spawnRate;
        public int groupSize;
    }
}