using Photon.Pun;
using UnityEngine;

public class LocationTransitionTrigger : MonoBehaviour
{
    [SerializeField] private string _mapName;
    [SerializeField] private LocationTypes _locationType;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            LoadingScreen.Instance.ShowLoadingScreen(_locationType);
            PhotonNetwork.LeaveRoom();
        
            Launcher.ActionBeforeMasterConnect = () =>
            {
                Launcher.JoinOrCreateRoom(_mapName);
            };
        }
    }
}
