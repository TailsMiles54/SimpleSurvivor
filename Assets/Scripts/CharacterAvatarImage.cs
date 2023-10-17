using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterAvatarImage : MonoBehaviour
{
    [SerializeField] private RawImage _targetImage;
    private Player _localPlayer;
    
    private void Start()
    {
        Player.PlayerInitialized += SetupCamera;
    }

    private void OnDestroy()
    {
        Player.PlayerInitialized -= SetupCamera;
    }

    private void SetupCamera(Player player)
    {
        _localPlayer = player;
        StartCoroutine(CheckCameraTexture());
    }

    private IEnumerator CheckCameraTexture()
    {
        while (_localPlayer.AvatarCamera.activeTexture == null)
        {
            yield return new WaitForEndOfFrame();
        }
        _targetImage.texture = _localPlayer.AvatarCamera.activeTexture;
    }
}
