using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class LoginGooglePlayGames : MonoBehaviour
{
    PlayGamesClientConfiguration _clientConfiguration;
    void Start()
    {
        ConfigurationGPGS();
        SiginToGPGS(SignInInteractivity.CanPromptOnce, _clientConfiguration);

    }
    void ConfigurationGPGS()
    {
        _clientConfiguration = new PlayGamesClientConfiguration.Builder().Build();
    }
    private void SiginToGPGS(SignInInteractivity inInteractivity, PlayGamesClientConfiguration clientConfiguration)
    {
        clientConfiguration = _clientConfiguration;
        PlayGamesPlatform.InitializeInstance(clientConfiguration);
        PlayGamesPlatform.Activate();

        PlayGamesPlatform.Instance.Authenticate(inInteractivity, (code) =>
        {
            if (code == SignInStatus.Success)
            {
                Debug.Log("đăng nhập thành công");
            }
            else
            {
                Debug.Log("fail");
            }
        });
    }
}
