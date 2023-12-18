using System.Collections.Generic;
using System.Linq;
using Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "EnemiesSetting", menuName = "SimpleSurvival/EnemiesSetting", order = 1)]
    public class EnemiesSetting : SerializedScriptableObject
    {
        [field: SerializeField] public List<EnemySettings> EnemiesSettingsList { get; private set; }

        public EnemySettings GetEnemyByType(EnemyTypes enemyTypes) =>
            EnemiesSettingsList.First(x => x.EnemyType == enemyTypes);
    }
}