using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Launcher : MonoBehaviourPunCallbacks
{
    public async void SetupData()
    {
        PhotonNetwork.NickName = await SaveDataManager.RetrieveSpecificData("character_name");
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
        
        PhotonNetwork.LoadLevel("SampleScene");
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
