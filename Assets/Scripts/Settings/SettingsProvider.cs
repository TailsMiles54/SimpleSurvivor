using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "SettingsProvider", menuName = "SimpleSurvival/SettingsProvider", order = 1)]
    public class SettingsProvider : SerializedScriptableObject
    {
        private static string _containerName = "SettingsProvider";
        private static Dictionary<Type, ScriptableObject> _settings;
        
        [SerializeField] private List<ScriptableObject> _settingsList;
        public List<ScriptableObject> SettingsList => _settingsList;

        [ContextMenu("Check list for identical types")]
        public void CheckTypes()
        {
            var types = new List<Type>();
            
            foreach (var s in _settingsList)
            {
                if (types.Contains(s.GetType()))
                {
                    Debug.LogError($"Found identical type: {types.Count()} - {s.GetType()}");
                }
                types.Add(s.GetType());
            }
            
        }
        
        private static void CheckSettings()
        {
            if (_settings != null)
                return;
            
            var settingsContainer = Resources.Load<SettingsProvider>(_containerName);
            SetupSettings(settingsContainer);
        }
        
        private static void SetupSettings(SettingsProvider settingsContainer)
        {
            try
            {
                _settings = settingsContainer.SettingsList.ToDictionary(x => x.GetType(), x => x);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }

        public static T Get<T>() where T : ScriptableObject
        {
            CheckSettings();

            if (_settings.ContainsKey(typeof(T)))
            {
                return (T)_settings[typeof(T)];
            }

            Debug.LogWarning($"Not found settings of type \"{typeof(T).FullName}\"");
            return null;
        }
    }
}