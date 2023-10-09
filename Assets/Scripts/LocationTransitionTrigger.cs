using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocationTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string _mapName;
    [SerializeField] private LocationTypes _locationType;
    
    private void OnTriggerEnter(Collider other)
    {
        LoadingScreen.Instance.ShowLoadingScreen(_locationType);
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();
        
        Launcher.ActionBeforeMasterLeave = () =>
        {
            SceneManager.LoadScene(0);
            PhotonNetwork.ConnectUsingSettings();
            Launcher.ActionBeforeMasterConnect = () =>
            {
                Launcher.Instance.JoinOrCreateRoom(_mapName);
                LoadingScreen.Instance.HideLoadingScreen();
            };
        };
    }
}
