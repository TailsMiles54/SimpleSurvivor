using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpellInfoElement : MonoBehaviour
{
    [SerializeField] private Image _spellIcon;
    [SerializeField] private TMP_Text _levelText;

    public void Setup(Sprite icon, int level)
    {
        _spellIcon.sprite = icon;
        _levelText.text = level.ToString();
    }
}