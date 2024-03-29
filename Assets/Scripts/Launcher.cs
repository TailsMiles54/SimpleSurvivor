using System;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Launcher : MonoBehaviourPunCallbacks
{
    public static Action ActionBeforeMasterConnect;
    public static Action ActionBeforeMasterLeave;

    [SerializeField] private Button _joinButton;
    
    public void SetupData(string nickname)
    {
        if(!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.NickName = nickname;
            PhotonNetwork.GameVersion = "1";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public void CreateRoom()
    {
        JoinOrCreateRoom("City");
    }

    public void JoinRoom() 
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public static void JoinOrCreateRoom(string mapName)
    {
        Dictionary<string, string> roomParams = new Dictionary<string, string>()
        {
            {"mapname", mapName}
        };

        Hashtable myHash = new Hashtable();

        foreach (var param in roomParams)
        {
            myHash.Add(param.Key, param.Value);
        }
        
        PhotonNetwork.JoinOrCreateRoom(mapName, new RoomOptions()
        {
            CustomRoomProperties = myHash,
        }, null, null);
    }

    public override void OnLeftLobby()
    {
        Log("Disconnect from lobby");
        if (ActionBeforeMasterLeave != null)
        {
            ActionBeforeMasterLeave.Invoke();
            ActionBeforeMasterLeave = null;
        }
    }

    public override void OnJoinedRoom()
    {
        Log("Joined to room");

        Debug.Log(PhotonNetwork.NetworkingClient.CurrentRoom.Name);
        var mapname = PhotonNetwork.CurrentRoom.CustomProperties["mapname"].ToString();
        PhotonNetwork.LoadLevel(mapname);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Log("Disconnect");
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
        if (ActionBeforeMasterConnect != null)
        {
            ActionBeforeMasterConnect.Invoke();
            ActionBeforeMasterConnect = null;
        }

        _joinButton.interactable = true;
    }

    public void Log(string message)
    {
        Debug.LogWarning(message);
    }
}
