using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Test : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;
    
    // Start is called before the first frame update
    void Start()
    {
        if (photonView.IsMine)
        {
            LocalPlayerInstance = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
