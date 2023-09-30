using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace DefaultNamespace
{
    public class CharacterEquipment : MonoBehaviour, IPunObservable
    {
        public List<EquipmentSlot> EquipmentSlots;
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            
        }
    }
}