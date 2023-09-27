using System;
using System.Collections.Generic;
using DefaultNamespace;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PlayerClassSettings", menuName = "SimpleSurvival/PlayerClassSettings", order = 1)]
    public class PlayerClassSettings : SerializedScriptableObject
    {
        [field: SerializeField]public List<PlayerClassSetting> Classes { get; private set; }
    }

    [Serializable]
    public struct PlayerClassSetting
    {
        public PlayerClasses Classes;
        public GameObject Prefab;
    }
}