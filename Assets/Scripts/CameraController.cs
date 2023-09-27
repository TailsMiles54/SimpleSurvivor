using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController _instance;

    public static CameraController Instance => _instance;

    private CinemachineVirtualCamera _virtualCamera;
    
    void Start()
    {
        _instance = this;
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    public void SetupCamera(GameObject gameObject)
    {
        _virtualCamera.Follow = gameObject.transform;
        _virtualCamera.LookAt = gameObject.transform;
    }
}
