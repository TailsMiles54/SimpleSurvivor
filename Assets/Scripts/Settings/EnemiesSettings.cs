using System.Collections.Generic;
using System.Linq;
using Enemies;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "EnemiesSetting", menuName = "SimpleSurvival/EnemiesSetting", order = 1)]
    public class EnemiesSetting : ScriptableObject
    {
        public List<EnemySettings> EnemiesSettingsList { get; private set; }

        public EnemySettings GetEnemyByType(EnemyTypes enemyTypes) =>
            EnemiesSettingsList.First(x => x.EnemyType == enemyTypes);
    }
}