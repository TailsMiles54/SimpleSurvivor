using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text LogText;
    [SerializeField] private GameObject _playerPrefab;
    
    void Start()
    {
        if (PhotonNetwork.InRoom && Test.LocalPlayerInstance==null)
        {
            var defPos = new Vector3(404.665558f, 0, 533.629211f);
            PhotonNetwork.Instantiate(_playerPrefab.name,
                new Vector3(defPos.x + Random.Range(-5, 5), 0,
                    defPos.z + Random.Range(-5, 5)), Quaternion.identity);
        }
    }
    
    public override void OnJoinedRoom()
    {
        if (Test.LocalPlayerInstance == null)
        {
            var defPos = new Vector3(404.665558f, 0, 533.629211f);
            PhotonNetwork.Instantiate(_playerPrefab.name,
                new Vector3(defPos.x + Random.Range(-5, 5), 0,
                    defPos.z + Random.Range(-5, 5)), Quaternion.identity);
        }
    }

    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Log($"Player {newPlayer.NickName} entered room");
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Log($"Player {otherPlayer.NickName} left room");
        PhotonNetwork.DestroyPlayerObjects(otherPlayer);
    }

    public void Log(string message)
    {
        Debug.Log(message);
        LogText.text += "\n";
        LogText.text += message;
    }
}
