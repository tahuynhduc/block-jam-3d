using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameController : GameBoard<Transform, UIType, UIPosition>
{
    LifeSystem lifeSystem;
    [SerializeField] GameObject _gameover;
    [SerializeField] GameObject _victory;
    [SerializeField] List<GameObject> _levelsUI;
    [SerializeField] Text _levels;
    private void OnEnable()
    {
        _levels.text = $"Level:{GameBoardController.level}";
        ShowLevel();
    }
    private void Start()
    {
        CreateUI();
        lifeSystem = FindObjectOfType<LifeSystem>();
    }
    public void ShowLevel()
    {
        for (int i = 0; i < _levelsUI.Count; i++)
        {
            _levelsUI[i].SetActive(true);
            if (GameBoardController.map == i)
                break;
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneController.LoadScene(sceneName);
        lifeSystem.LoadGamePlay();
    }
    public void PLayOnBtn(string sceneName)
    {
        DataUser.MinusCoins(100);
        SceneController.LoadScene(sceneName);
    }
    public void ShowGameOver()
    {
        //GameBoardController.map = 0;
        _gameover.SetActive(true);
    }
    public void ShowVictory()
    {
        _victory.SetActive(true);
    }
}
public enum UIType
{
    None,
    Wall,
    GroundLeft,
    GroundRight,
    GroundTop,
    UI,
}
public class SceneController
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}