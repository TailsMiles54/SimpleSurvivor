using System;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class AppearanceController : MonoBehaviour
{
    [SerializeField] private Transform _controllersParent;
    [SerializeField] private ChangeAppearanceController _controllerPrefab;
    [SerializeField] private CharacterAppearance _characterAppearance;

    public void Start()
    {
        Setup();
    }

    private void Setup()
    {
        foreach (var appearanceSlot in _characterAppearance.AppearanceSlots)
        {
            var controller = Instantiate(_controllerPrefab, _controllersParent);
            controller.Setup(appearanceSlot);
        }
    }
}