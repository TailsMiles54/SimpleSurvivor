using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController _instance;
    public static UIController Instance => _instance; 

    [field: SerializeField] public Slider HealthSlider { get; private set; }
    [field: SerializeField] public Slider MainLevel { get; private set; }
    [field: SerializeField] public Slider JobLevel { get; private set; }
    [field: SerializeField] public TMP_Text NickName { get; private set; }
    
    void Start()
    {
        _instance = this;
    }
}
