using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "WeaponSetting", menuName = "SimpleSurvival/WeaponSetting", order = 1)]
    public class WeaponSetting : SerializedScriptableObject
    {
        [ShowInInspector]public int PoolLimit { get; set; }
        [ShowInInspector]public int Amount { get; set; }
        [ShowInInspector]public float MaxLevel { get; set; }
        [ShowInInspector]public float KnockBack { get; set; }
        [ShowInInspector]public bool BlockByWalls { get; set; }
        [ShowInInspector]public List<IWeaponEffect> WeaponEffects { get; set; }
        [ShowInInspector][HorizontalGroup("Test1")]
        public float BaseAttackSpeed { get; set; }
        [ShowInInspector][HorizontalGroup("Test1")]
        public float BaseAttackDamage { get; set; }
        [ShowInInspector][HorizontalGroup("Test1")]
        public float BaseAttackDuration { get; set; }
        [ShowInInspector][HorizontalGroup("Test2")]
        public float BaseAttackCooldown { get; set; }
        [ShowInInspector][HorizontalGroup("Test2")]
        public float BaseHitBoxDelay { get; set; }
    }
}