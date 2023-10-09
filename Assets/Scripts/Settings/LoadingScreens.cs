using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "LoadingScreens", menuName = "SimpleSurvival/LoadingScreens", order = 1)]
    public class LoadingScreens : SerializedScriptableObject
    {
        [field: SerializeField] public List<ScreenForLocation> ScreenForLocations { get; private set; }
    }

    [Serializable]
    public struct ScreenForLocation
    {
        public LocationTypes Location;
        public List<Sprite> Backgrounds;
    }
}