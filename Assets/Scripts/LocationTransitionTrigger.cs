using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string _mapName; 
    
    private void OnTriggerEnter(Collider other)
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        
        Launcher.ActionBeforeMasterLeave = () =>
        {
            SceneManager.LoadScene(0);
            PhotonNetwork.ConnectUsingSettings();
            Launcher.ActionBeforeMasterConnect = () =>
            {
                Launcher.Instance.JoinOrCreateRoom(_mapName);
            };
        };
    }
}
