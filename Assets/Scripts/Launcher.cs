using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text LogText;
    [SerializeField] private ToggleGroup _toggleGroup;
    
    void Start()
    {
        PhotonNetwork.NickName = Guid.NewGuid().ToString();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 2 });
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Log("Joined to room");

        PlayerInfo.PlayerClass = _toggleGroup.ActiveToggles().First().GetComponent<ClassToggle>().PlayerClass;
        
        PhotonNetwork.LoadLevel("SampleScene");
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
    }

    public void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }
}
