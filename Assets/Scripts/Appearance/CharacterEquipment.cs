using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using UnityEngine;

namespace DefaultNamespace
{
    public class CharacterEquipment : MonoBehaviour, IPunObservable
    {
        public List<EquipmentSlot> EquipmentSlots;
        
        public async void LoadAppearance(bool isMine = true)
        {
            if(isMine)
            {
                foreach (var appearanceSlot in EquipmentSlots)
                {
                    appearanceSlot.ItemId =
                        await SaveDataManager.RetrieveSpecificData(appearanceSlot.SlotType.ToString()) ??
                        string.Empty;
                }
                SetupAppearance();
            }
        }

        public void SetupAppearance()
        {
            foreach (var appearanceSlot in EquipmentSlots)
            {
                appearanceSlot.ChangeElement();
            }
        }
        
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting) 
            {
                foreach (var appearanceSlot in EquipmentSlots)
                {
                    stream.SendNext (appearanceSlot.SlotType);
                    stream.SendNext (appearanceSlot.ItemId);
                }
            } 
            else
            {
                for (int i = 0; i < EquipmentSlots.Count; i++)
                {
                    var equipmentSlotType = (EquipmentSlotType)stream.ReceiveNext();
                    var appearanceId = (string)stream.ReceiveNext();

                    var currentSlot = EquipmentSlots.First(x => x.SlotType == equipmentSlotType);
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
    public class EquipmentSlot
    {
        public EquipmentSlotType SlotType;
        public List<EquipmentElement> AllowedElements; 
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
}