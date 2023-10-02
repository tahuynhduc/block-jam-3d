using UnityEngine;

public class UIGameController : GameBoard<Transform, UIType, UIPosition>
{
    private void Start()
    {
        CreateUI();
    }
}
public enum UIType
{
    None,
    Wall,
    GroundLeft,
    GroundRight,
    GroundTop,
}