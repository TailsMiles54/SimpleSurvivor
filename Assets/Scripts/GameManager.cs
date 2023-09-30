using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using Photon.Pun;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviourPunCallbacks
{
    [FormerlySerializedAs("LogText")] [SerializeField] private TMP_Text _logText;
    [SerializeField] private GameObject _playerPrefab;

    void Start()
    {
        if (PhotonNetwork.InRoom && Player.LocalPlayerInstance==null)
        {
             SpawnCharacter();
        }
    }
    
    public override void OnJoinedRoom()
    {
        if (Player.LocalPlayerInstance == null)
        {
            SpawnCharacter();
        }
    }

    private void SpawnCharacter()
    {
        var defPos = new Vector3(404.665558f, 0, 533.629211f);
        
        PhotonNetwork.Instantiate(_playerPrefab.name,
            new Vector3(defPos.x + Random.Range(-5, 5), 0,
                defPos.z + Random.Range(-5, 5)), Quaternion.identity);
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
        _logText.text += "\n";
        _logText.text += message;
    }
}