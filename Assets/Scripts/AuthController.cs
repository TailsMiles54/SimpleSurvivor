using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class AuthController : MonoBehaviour
{
    [SerializeField] private GameObject _authPanel;
    [SerializeField] private GameObject _lobbyPanel;
    [SerializeField] private GameObject _characterPanel;

    [SerializeField] private Launcher _launcher;
    
    [SerializeField] private TMP_InputField _characterName;
    [SerializeField] private CharacterAppearance _characterAppearance;

    [SerializeField] private TMP_Text _characterShowedName;
    
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
            foreach (var appearanceSlot in _characterAppearance.AppearanceSlots)
            {
                SaveDataManager.Save(appearanceSlot.AppearanceType.ToString(), appearanceSlot.ItemId);
            }
            _lobbyPanel.SetActive(true);
            _characterPanel.SetActive(false);
            _launcher.SetupData(_characterName.text);
            _characterShowedName.text = _characterName.text;
            
            SaveDataManager.Save("Name", _characterName.text);
        }
    }

    public void SetupEvents() {
        AuthenticationService.Instance.SignedIn += async () => {
            Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");
            Debug.Log($"Access Token: {AuthenticationService.Instance.AccessToken}");

            _authPanel.SetActive(false);
            _characterAppearance.LoadAppearance();
            
            var loadedUserName = await SaveDataManager.RetrieveSpecificData("Name");
            
            if (string.IsNullOrEmpty(loadedUserName))
            {
                _lobbyPanel.SetActive(false);
                _characterPanel.SetActive(true);
            }
            else
            {
                _characterShowedName.text = loadedUserName;
                _lobbyPanel.SetActive(true);
                _launcher.SetupData(loadedUserName);
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
            AuthenticationService.Instance.ClearSessionToken();
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
