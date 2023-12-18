using System;
using System.Collections.Generic;
using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SpawnSettings", menuName = "SimpleSurvival/SpawnSettings", order = 1)]
    public class SpawnSettings : SerializedScriptableObject
    {
        [field: SerializeField] public float MinRadius { get; private set; }
        [field: SerializeField] public float SpawnRadius { get; private set; }
        [field: SerializeField] public List<WaveSpawnSettings> WaveSpawnSettingsList { get; private set; }
    }

    [Serializable]
    public struct WaveSpawnSettings
    {
        public List<EnemySpawnSetting> EnemiesSpawnSettings;
        public float EnemySpawnDelay;
    }

    [Serializable]
    public struct EnemySpawnSetting
    {
        public EnemyTypes EnemyTypes;
        public int EnemyCount;
    }
}