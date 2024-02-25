using System;
using System.Collections.Generic;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "SimpleSurvival/LevelSettings", order = 1)]
    public class LevelSettings : ScriptableObject
    {
        [field: SerializeField] public List<LevelSetting> MainLevels { get; private set; }
        [field: SerializeField] public List<LevelSetting> JobLevels { get; private set; }
    }

    [Serializable]
    public struct LevelSetting
    {
        public int MaxExp;
    }
}