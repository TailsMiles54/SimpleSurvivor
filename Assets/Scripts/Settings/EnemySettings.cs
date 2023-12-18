using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "SimpleSurvival/EnemySettings", order = 1)]
    public class EnemySettings : SerializedScriptableObject
    {
        [field: SerializeField] public EnemyTypes EnemyType { get; private set; }
        [field: SerializeField] public GameObject EnemyPrefab { get; private set; }
    }
}