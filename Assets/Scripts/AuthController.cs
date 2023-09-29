using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.UI;

public class AuthController : MonoBehaviour
{
    [SerializeField] private GameObject _authPanel;
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private GameObject _characterPanel;

    [SerializeField] private TMP_InputField _characterName;
    [SerializeField] private CharacterAppearance _characterAppearance;
    
    
    
    [SerializeField] private TMP_InputField _login;
    [SerializeField] private TMP_InputField _password;
    
    async void Awake()
    {
        try
        {
            await UnityServices.InitializeAsync();
            SetupEvents();
            SaveDataManager.Setup();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public async void Register()
    {
        Debug.Log(_login.text + "   " + _password.text);
        await SignUpWithUsernamePasswordAsync(_login.text, _password.text);
    }

    public async void Login()
    {
        await SignInWithUsernamePasswordAsync(_login.text, _password.text);
    }

    public void Exit()
    {
        SignOutWithUsernamePasswordAsync();
    }

    public void SaveCharacter()
    {
        if (!string.IsNullOrEmpty(_characterName.text))
        {
            SaveDataManager.Save("character_name", _characterName.text);
            foreach (var appearanceSlot in _characterAppearance.AppearanceSlots)
            {
                SaveDataManager.Save(appearanceSlot.AppearanceType.ToString(), appearanceSlot.ItemId);
            }
            _lobbyPanel.SetActive(true);
            _characterPanel.SetActive(false);
        }
    }

    public void SetupEvents() {
        AuthenticationService.Instance.SignedIn += async () => {
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

            _authPanel.SetActive(false);
            var nickName = await SaveDataManager.RetrieveSpecificData("character_name");

            _characterAppearance.LoadAppearance();
            
            if (string.IsNullOrEmpty(nickName))
            {
                _lobbyPanel.SetActive(false);
                _characterPanel.SetActive(true);
            }
            else
            {
                _lobbyPanel.SetActive(true);
                _characterPanel.SetActive(false);
            }
        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            _authPanel.SetActive(true);
            _lobbyPanel.SetActive(false);
            _characterPanel.SetActive(false);
            Debug.Log("Player signed out.");
        };

        AuthenticationService.Instance.Expired += () =>
        {
            Debug.Log("Player session could not be refreshed and expired.");
        };
    }
    
    async Task SignUpWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            Debug.Log("SignUp is successful.");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
    
    async Task SignInWithUsernamePasswordAsync(string username, string password)
    {
        try
        {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            Debug.Log("SignIn is successful.");
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
    
    public void SignOutWithUsernamePasswordAsync()
    {
        try
        {
            AuthenticationService.Instance.SignOut();
        }
        catch (AuthenticationException ex)
        {
            Debug.LogException(ex);
        }
        catch (RequestFailedException ex)
        {
            Debug.LogException(ex);
        }
    }
}
