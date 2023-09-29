using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.UI;

public class CharacterCreatorImage : MonoBehaviour
{
    [SerializeField] private Camera _characterCreatorCamera;
    [SerializeField] private RawImage _targetImage;

    private void Update()
    {
        _targetImage.texture = _characterCreatorCamera.activeTexture;
    }
}
