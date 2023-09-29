using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAppearanceController : MonoBehaviour
{
    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonNext;
    [SerializeField] private TMP_Text _text;

    private AppearanceSlot _appearanceSlot;
    
    private int CurrentItemIndex()
    {
        AppearanceElement currentElement = null;

        foreach (var appearanceElement in _appearanceSlot.AllowedElements)
        {
            if (appearanceElement.Id == _appearanceSlot.ItemId)
            {
                currentElement = appearanceElement;
                break;
            }
        }
        
        if (currentElement == null)
            return 0;
        
        int index = _appearanceSlot.AllowedElements.IndexOf(currentElement);
        
        return index;
    }
    
    public void Setup(AppearanceSlot appearanceSlot)
    {
        _appearanceSlot = appearanceSlot;
        _text.text = $"{_appearanceSlot.AppearanceType.ToString()} {CurrentItemIndex()+1}/{_appearanceSlot.AllowedElements.Count}";
        
        _buttonBack.onClick.AddListener(() =>
        {
            var test = CurrentItemIndex()-1;
            var newIndex = test < 0 ? appearanceSlot.AllowedElements.Count-1 : test;
            SetElement(newIndex);
        });
        
        _buttonNext.onClick.AddListener(() =>
        {
            var test = CurrentItemIndex()+1;
            var newIndex = test > appearanceSlot.AllowedElements.Count-1 ? 0 : test;
            SetElement(newIndex);
        });
    }

    private void SetElement(int newIndex)
    {
        _appearanceSlot.Set(newIndex);

        _text.text = $"{_appearanceSlot.AppearanceType.ToString()} {CurrentItemIndex()+1}/{_appearanceSlot.AllowedElements.Count}";
    }
}