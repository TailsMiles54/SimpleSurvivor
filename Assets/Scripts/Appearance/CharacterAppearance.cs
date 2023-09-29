using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class CharacterAppearance : MonoBehaviour
{
    public List<AppearanceSlot> AppearanceSlots;
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
        
        foreach (var appearanceElement in AllowedElements)
        {
            appearanceElement.gameObject.SetActive(appearanceElement.Id == ItemId);
        }
    }
}