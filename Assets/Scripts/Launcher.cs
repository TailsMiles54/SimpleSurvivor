using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using Sirenix.OdinInspector;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    private static Launcher _instance;
    public static Launcher Instance => _instance;

    public static Action ActionBeforeMasterConnect;
    public static Action ActionBeforeMasterLeave;
    
    public async void SetupData()
    {
        _instance = this;
        PhotonNetwork.NickName = await SaveDataManager.RetrieveSpecificData("character_name");
        PhotonNetwork.GameVersion = "1";
        PhotonNetwork.ConnectUsingSettings();
        DontDestroyOnLoad(gameObject);
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(null, new RoomOptions
        {
            CustomRoomPropertiesForLobby = new string[]
            {
            },
        });
    }

    public void JoinRoom() 
    {
        PhotonNetwork.JoinRandomRoom();
    }

    [Button]
    public void JoinOrCreateRoom(string mapName)
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
        
        PhotonNetwork.JoinOrCreateRoom(Guid.NewGuid().ToString(), new RoomOptions()
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
        if (ActionBeforeMasterLeave != null)
        {
            ActionBeforeMasterLeave.Invoke();
            ActionBeforeMasterLeave = null;
        }
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
        if (ActionBeforeMasterConnect != null)
        {
            ActionBeforeMasterConnect.Invoke();
            ActionBeforeMasterConnect = null;
        }
    }

    public void Log(string message)
    {
        Debug.LogWarning(message);
    }
}
