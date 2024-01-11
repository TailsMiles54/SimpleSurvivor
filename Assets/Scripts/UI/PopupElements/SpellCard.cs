using System;
using Enums;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellCard : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Image _spellIcon;
    [SerializeField] private TMP_Text _spellUpdateText;
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _selectBorder;
    
    public void Setup(SpellTypes spell, int currentLevel, Action action)
    {
        _button.onClick.AddListener(action.Invoke);
        
        var settings = SettingsProvider.Get<SpellsSettings>().GetSpell(spell);
        _nameText.text = settings.SpellTypes.ToString();
        _spellIcon.sprite = settings.Icon;
        
        string updateSpellText = String.Empty;

        var spellStats = settings.SpellLevelSettings;
        
        if(spellStats[currentLevel].BaseDamage != spellStats[currentLevel+1].BaseDamage){ updateSpellText += $"BaseDamage: {spellStats[currentLevel].BaseDamage} => <color=green>{spellStats[currentLevel+1].BaseDamage}</color>\n";} 
        if(spellStats[currentLevel].Speed != spellStats[currentLevel+1].Speed){ updateSpellText += $"Speed: {spellStats[currentLevel].Speed} => <color=green>{spellStats[currentLevel+1].Speed}</color>\n";} 
        if(spellStats[currentLevel].Duration != spellStats[currentLevel+1].Duration){ updateSpellText += $"Duration: {spellStats[currentLevel].Duration} => <color=green>{spellStats[currentLevel+1].Duration}</color>\n";} 
        if(spellStats[currentLevel].Cooldown != spellStats[currentLevel+1].Cooldown){ updateSpellText += $"Cooldown: {spellStats[currentLevel].Cooldown} => <color=green>{spellStats[currentLevel+1].Cooldown}</color>\n";} 
        if(spellStats[currentLevel].PoolLimit != spellStats[currentLevel+1].PoolLimit){ updateSpellText += $"PoolLimit: {spellStats[currentLevel].PoolLimit} => <color=green>{spellStats[currentLevel+1].PoolLimit}</color>\n";} 
        if(spellStats[currentLevel].Amount != spellStats[currentLevel+1].Amount){ updateSpellText += $"Amount: {spellStats[currentLevel].Amount} => <color=green>{spellStats[currentLevel+1].Amount}</color>\n";} 
        if(spellStats[currentLevel].Pierce != spellStats[currentLevel+1].Pierce){ updateSpellText += $"Pierce: {spellStats[currentLevel].Pierce} => <color=green>{spellStats[currentLevel+1].Pierce}</color>\n";} 
        
        _spellUpdateText.text = updateSpellText;
    }

    public void SetActive(bool state)
    {
        _selectBorder.SetActive(state);
    }
}
