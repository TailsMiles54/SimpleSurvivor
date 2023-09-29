using System.Collections.Generic;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using UnityEngine;

public static class SaveDataManager
{
    public static async void Setup()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
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
}
