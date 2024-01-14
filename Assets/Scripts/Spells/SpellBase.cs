using System;
using Enums;
using Settings;
using Settings.Spells;
using UnityEngine;

public class SpellBase : MonoBehaviour
{
    public Player Parent;
    [SerializeField] private int _level;
    public bool Active { get; private set; }
    [field: SerializeField] public GameObject SpellPrefab { get; private set; }
    [field: SerializeField] public SpellTypes SpellType { get; private set; }
    public BaseSpellSetting SpellSettings { get; private set; }

    public SpellLevelSetting GetLevelSetting => SpellSettings.SpellLevelSettings[_level];

    public void Awake()
    {
        SpellSettings = SettingsProvider.Get<SpellsSettings>().GetSpell(SpellType);
    }

    public void LevelUp()
    {
        _level++;
        
        if (_level != 0)
        {
            Activate();
        }
    }

    private void FixedUpdate()
    {
        if (_level != 0 && !Active)
        {
            Active = true;
            Activate();
        }
    }

    public void SetActive(bool state)
    {
        Active = state;
    }

    public int GetLevel => _level;

    public virtual void Activate() { }
}
