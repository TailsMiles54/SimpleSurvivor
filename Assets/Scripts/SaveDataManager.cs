using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;

public static class SaveDataManager
{
    private static bool _instantiated;
    
    public static async void Setup()
    {
        if(!_instantiated)
        {
            await UnityServices.InitializeAsync();
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            _instantiated = true;
        }
    }

    public static async void Save(string key, object data)
    {
        try
        {
            var saveData = new Dictionary<string, object> { { key, data } };
            await CloudSaveService.Instance.Data.ForceSaveAsync(saveData);
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }
    }

    public static async Task<string> RetrieveSpecificData(string key)
    {
        try
        {
            var results = await CloudSaveService.Instance.Data.LoadAsync(new HashSet<string> {key});

            if (results.TryGetValue(key, out var item))
            {
                return item;
            }
            else
            {
                Debug.Log($"There is no such key as {key}!");
            }
        }
        catch (CloudSaveValidationException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveRateLimitedException e)
        {
            Debug.LogError(e);
        }
        catch (CloudSaveException e)
        {
            Debug.LogError(e);
        }

        return default;
    }
}
