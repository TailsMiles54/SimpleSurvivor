using System.Collections.Generic;
using System.Linq;
using Character;
using Enums;
using UnityEngine;

public class LevelRewardPopup : Popup<LevelRewardPopupSettings>
{
    private SpellTypes? _selectedSpellType = null;
    [SerializeField] private Transform _spellCardsParent;
    [SerializeField] private SpellCard _spellCard;

    private Player _player;

    private List<SpellCard> _spellCards = new List<SpellCard>();
    
    public override void Setup(LevelRewardPopupSettings settings)
    {
        foreach (var spell in settings.Spells)
        {
            var spellObject = Instantiate(_spellCard, _spellCardsParent);

            var players = FindObjectsOfType<Player>();
            _player = players.First(x => x.photonView.IsMine);

            var userSpellData = _player.UserInfo.GetSpellData(spell);
            _spellCards.Add(spellObject);
            
            spellObject.Setup(spell, userSpellData?.CurrentLevel ?? 0, () =>
            {
                foreach (var spellCard in _spellCards)
                {
                    spellCard.SetActive(spellCard == spellObject);
                }
                _selectedSpellType = spell;
            });
        }
    }

    public void Select()
    {
        if (_selectedSpellType != null)
        {
            if (_player.UserInfo.UserSpellsData.Any(x => x.SpellTypes == _selectedSpellType))
            {
                _player.UserInfo.UserSpellsData.First(x => _selectedSpellType == x.SpellTypes).CurrentLevel++;
            }
            else
            {
                _player.UserInfo.UserSpellsData.Add(new SpellData()
                {
                    SpellTypes = _selectedSpellType,
                    CurrentLevel = 1
                });
            }
            PopupController.Instance.HidePopup();
        }
    }

    public void Close()
    {
        PopupController.Instance.HidePopup();
    }
}

public class LevelRewardPopupSettings : BasePopupSettings
{
    public List<SpellTypes> Spells;
}

public class BasePopupSettings
{
    
}