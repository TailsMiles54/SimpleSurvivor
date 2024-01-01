using UnityEngine;

public class PopupController : MonoBehaviour
{
    private static PopupController _instance;
    public static PopupController Instance => _instance;

    [SerializeField] private GameObject _background;
    
    void Awake()
    {
        _instance = this;
    }
}
