using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Settings;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Image _backgroundImage;
    [SerializeField] private GameObject _canvas;
    private static LoadingScreen _instance;
    public static LoadingScreen Instance => _instance;
    
    void Start()
    {
        _instance = this;
        DontDestroyOnLoad(_canvas);
        gameObject.SetActive(false);
    }

    [Button]
    public void DontDestroy()
    {
        DontDestroyOnLoad(_canvas);
    }
    
    public void ShowLoadingScreen(LocationTypes locationType)
    {
        _backgroundImage.sprite = SettingsProvider.Get<LoadingScreens>().ScreenForLocations
            .First(x => x.Location == locationType).Backgrounds.GetRandomElement();
        gameObject.SetActive(true);
    }

    public void HideLoadingScreen()
    {
        gameObject.SetActive(false);
    }
}
