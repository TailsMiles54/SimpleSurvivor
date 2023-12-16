using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SpawnSettings", menuName = "SimpleSurvival/SpawnSettings", order = 1)]
    public class SpawnSettings : SerializedScriptableObject
    {
        [field: SerializeField] public float MinRadius { get; private set; }
        [field: SerializeField] public float SpawnRadius { get; private set; }
    }
}