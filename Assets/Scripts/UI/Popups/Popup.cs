using UnityEngine;

public class Popup<BasePopupSettings> : BasePopup
{
    public virtual void Setup(BasePopupSettings settings)
    {
        
    }
}

public class BasePopup : MonoBehaviour
{
    public virtual void Hide()
    {
        Destroy(gameObject);
    }
}