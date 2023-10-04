using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private static UIController _instance;
    public static UIController Instance => _instance; 

    [field: SerializeField] public Slider HealthSlider { get; private set; }
    
    void Start()
    {
        _instance = this;
    }

    
}
