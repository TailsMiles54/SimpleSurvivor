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
    [SerializeField] private GameObject _connectPanel;
    [SerializeField] private GameObject _characterPanel;
    
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

    void SetupEvents() {
        AuthenticationService.Instance.SignedIn += () => {
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");
            _authPanel.SetActive(false);
            _connectPanel.SetActive(false);
            _characterPanel.SetActive(true);
        };

        AuthenticationService.Instance.SignInFailed += (err) => {
            Debug.LogError(err);
        };

        AuthenticationService.Instance.SignedOut += () => {
            _authPanel.SetActive(true);
            _connectPanel.SetActive(false);
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
