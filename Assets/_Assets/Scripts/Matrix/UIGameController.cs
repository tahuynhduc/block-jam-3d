using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameController : GameBoard<Transform, UIType, UIPosition>
{
    LifeSystem lifeSystem;
    [SerializeField] GameObject _gameover;
    [SerializeField] GameObject _victory;
    private void Start()
    {
        CreateUI();
        lifeSystem = FindObjectOfType<LifeSystem>();
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
        GameBoardController.map = 0;
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