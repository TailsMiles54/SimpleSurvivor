using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Newtonsoft.Json;
using Photon.Pun;
using UnityEngine;

public class CharacterAppearance : MonoBehaviour, IPunObservable
{
    public List<AppearanceSlot> AppearanceSlots;
    
    public async void LoadAppearance(bool isMine = true)
    {
        if(isMine)
        {
            foreach (var appearanceSlot in AppearanceSlots)
            {
                appearanceSlot.ItemId =
                    await SaveDataManager.RetrieveSpecificData(appearanceSlot.AppearanceType.ToString()) ??
                    string.Empty;
            }
            SetupAppearance();
            LoadingScreen.Instance.HideLoadingScreen();
        }
    }

    public void SetupAppearance()
    {
        foreach (var appearanceSlot in AppearanceSlots)
        {
            appearanceSlot.ChangeElement();
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) 
        {
            foreach (var appearanceSlot in AppearanceSlots)
            {
               stream.SendNext (appearanceSlot.AppearanceType);
               stream.SendNext (appearanceSlot.ItemId);
            }
        } 
        else
        {
            for (int i = 0; i < AppearanceSlots.Count; i++)
            {
                var appearanceType = (AppearanceType)stream.ReceiveNext();
                var appearanceId = (string)stream.ReceiveNext();

                var currentSlot = AppearanceSlots.First(x => x.AppearanceType == appearanceType);
                if (currentSlot.ItemId != appearanceId)
                {
                    currentSlot.ItemId = appearanceId;
                    currentSlot.ChangeElement();
                }
            }
        }
    }
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
        if (string.IsNullOrEmpty(ItemId))
        {
            ItemId = AllowedElements.First().Id;
        }
        
        foreach (var appearanceElement in AllowedElements)
        {
            appearanceElement.gameObject.SetActive(appearanceElement.Id == ItemId);
        }
    }
}