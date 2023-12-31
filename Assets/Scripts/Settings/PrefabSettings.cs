using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PrefabSettings", menuName = "SimpleSurvival/PrefabSettings", order = 1)]
    public class PrefabSettings : ScriptableObject
    {
        [SerializeField] private List<BasePopup> _popups;
        
        public T GetPopup<T>() where T : BasePopup
        {
            return (T)_popups.First(x => x is T);
        }
    }
}