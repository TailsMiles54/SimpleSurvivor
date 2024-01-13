using System.Collections.Generic;
using System.Linq;
using Enums;
using UnityEngine;

public class SpellsController : MonoBehaviour
{
    [SerializeField] private Player _player;

    [SerializeField] private List<SpellBase> _spells;

    public void SpellUpgrade(SpellTypes? spellType)
    {
        _spells.First(x => x.SpellType == spellType).LevelUp();
    }
}
