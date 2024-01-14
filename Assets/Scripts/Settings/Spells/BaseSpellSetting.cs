using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Settings.Spells
{
    [CreateAssetMenu(fileName = "BaseSpellSetting", menuName = "SimpleSurvival/BaseSpellSetting", order = 1)]
    public class BaseSpellSetting : ScriptableObject
    {
        public int MaxLevel => SpellLevelSettings.Count;
        [field: SerializeField] public SpellTypes SpellTypes { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public List<SpellLevelSetting> SpellLevelSettings { get; private set; }
    }

    [Serializable]
    public class SpellLevelSetting
    {
        public float BaseDamage;
        public float Speed;
        public float Duration;
        public float Cooldown;
        public float Radius;
        public int PoolLimit;
        public int Amount;
        public int Pierce; //Колво врагов
    }
}