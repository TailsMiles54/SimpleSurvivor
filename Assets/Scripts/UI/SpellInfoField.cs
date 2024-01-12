using System.Collections;
using System.Collections.Generic;
using Character;
using Enums;
using Settings;
using UnityEngine;

public class SpellInfoField : MonoBehaviour
{
    [SerializeField] private SpellInfoElement _spellInfoPrefab;
    
    public void ShowSpells(List<SpellData> userSpellsData)
    {
        foreach (var spellData in userSpellsData)
        {
            if (spellData.SpellTypes != null)
            {
                var spellSetting = SettingsProvider.Get<SpellsSettings>().GetSpell((SpellTypes)spellData.SpellTypes);

                var spellObject = Instantiate(_spellInfoPrefab, transform);
                spellObject.Setup(spellSetting.Icon, spellData.CurrentLevel);
            }
        }
    }
}