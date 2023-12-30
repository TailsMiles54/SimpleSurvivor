using System.Collections.Generic;
using System.Linq;
using Settings.Spells;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SpellsSettings", menuName = "SimpleSurvival/SpellsSettings", order = 1)]
    public class SpellsSettings : ScriptableObject
    {
        [field: SerializeField] public int TotalSpellsOnCharacter { get; private set; }
        [field: SerializeField] public List<BaseSpellSetting> SpellSettings { get; private set; }

        public BaseSpellSetting GetSpell(SpellTypes spellType) => SpellSettings.First(x => x.SpellTypes == spellType);
    }
}