using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CharacterAppearance : MonoBehaviour
{
    public List<AppearanceSlot> AppearanceSlots;

    public async void LoadAppearance()
    {
        foreach (var appearanceSlot in AppearanceSlots)
        {
            appearanceSlot.ItemId = await SaveDataManager.RetrieveSpecificData(appearanceSlot.AppearanceType.ToString()) ?? string.Empty;
            appearanceSlot.ChangeElement();
        }
    }
}

[Serializable]
public class EquipmentSlot
{
    public EquipmentSlotType SlotType;
    public List<EquipmentElement> AllowedElements; 
    public string ItemId;
}

[Serializable]
public class AppearanceSlot
{
    public AppearanceType AppearanceType;
    public List<AppearanceElement> AllowedElements; 
    public string ItemId; 

    public void Set(int id)
    {
        ItemId = AllowedElements[id].Id;
        ChangeElement();
    }

    public void ChangeElement()
    {
        foreach (var appearanceElement in AllowedElements)
        {
            appearanceElement.gameObject.SetActive(appearanceElement.Id == ItemId);
        }
    }
}