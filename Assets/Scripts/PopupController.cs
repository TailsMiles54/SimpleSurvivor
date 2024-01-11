using System;
using System.Collections.Generic;
using System.Linq;
using Settings;
using Sirenix.Utilities;
using UnityEngine;

public class PopupController : MonoBehaviour
{
    private static PopupController _instance;
    public static PopupController Instance => _instance;

    [SerializeField] private GameObject _background;
    [SerializeField] private Transform _popupParent;

    private BasePopup _currentPopup;
    
    void Awake()
    {
        _instance = this;
    }

    public void ShowPopup<T>(T settings) where T : BasePopupSettings
    {
        if(_currentPopup == null)
        {
            var popupPrefab = SettingsProvider.Get<PrefabSettings>().GetPopup<Popup<T>>();
            var instance = Instantiate(popupPrefab, _popupParent, false);
            instance.Setup(settings);
            _currentPopup = instance;
            _background.SetActive(true);
        }
    }

    public void HidePopup()
    {
        _currentPopup.Hide();
        _currentPopup = null;
        _background.SetActive(false);
    }
}
