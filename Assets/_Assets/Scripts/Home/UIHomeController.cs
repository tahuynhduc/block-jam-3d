using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHomeController : MonoBehaviour
{
    public void OnClickGamePlay(string sceneName)
    {
        SceneController.LoadScene(sceneName);
    }
    protected void ShowCoins(Text coins)
    {
        coins.text = DataUser.Coins.ToString();
    }
    public void SetCurrentLevel()
    {
        GameBoardController.level = 1;
        GameBoardController.map = 1;
    }
}
public enum TypeBtn
{
    Home,
    Setting,
    Rank,
    Shop,
}
