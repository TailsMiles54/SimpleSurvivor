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
    
    public async void SetupData()
    {
        _instance = this;
        PhotonNetwork.NickName = await SaveDataManager.RetrieveSpecificData("character_name");
        PhotonNetwork.AutomaticallySyncScene = true;
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

    public override void OnJoinedRoom()
    {
        Log("Joined to room");

        Debug.Log(PhotonNetwork.NetworkingClient.CurrentRoom.Name);
        var mapname = PhotonNetwork.CurrentRoom.CustomProperties["mapname"].ToString();
        PhotonNetwork.LoadLevel(mapname);
    }

    public override void OnConnectedToMaster()
    {
        Log("Connected to Master");
    }

    public void Log(string message)
    {
        Debug.Log(message);
    }
}
