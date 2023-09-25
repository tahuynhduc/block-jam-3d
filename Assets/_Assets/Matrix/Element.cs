using System;
using UnityEngine;

public class Element<TTransform, TElementType, TElement> : MonoBehaviour, IIndex where TElement : MonoBehaviour, IIndex
{
    [SerializeField] private MatrixIndex _index;
    private GameBoard<TTransform, TElementType, TElement> _gameBoard;
    public GameBoard<TTransform, TElementType, TElement> GameBoard
    {
        get
        {
            if (!_gameBoard)
                _gameBoard = FindObjectOfType<GameBoard<TTransform, TElementType, TElement>>();
            return _gameBoard;
        }
    }

    public MatrixIndex Index { get => _index; set => _index = value; }
    public int Row => Index.Column;
    public int Column => Index.Row;

    public TElement At => GameBoard.At<TElement>(Index);
    public TElement Left => GameBoard.LeftOf<TElement>(Index);
    public TElement Right => GameBoard.RightOf<TElement>(Index);
    public TElement Up => GameBoard.UpOf<TElement>(Index);
    public TElement Down => GameBoard.DownOf<TElement>(Index);
    public TElement UpLeft => GameBoard.UpLeftOf<TElement>(Index);
    public TElement UpRight => GameBoard.UpRightOf<TElement>(Index);
    public TElement DownRight => GameBoard.DownRightOf<TElement>(Index);
    public TElement DownLeft => GameBoard.DownLeftOf<TElement>(Index);
    public void SetIndexElementType(TElementType Type, TElement element)
    {
        var IndexElementType = GameBoard.ElementTypeMatrix;
        var IndexElement = GameBoard.ElementMatrix;
        IndexElementType.Set(Index, Type);
        //IndexElement.Set(Index, element);
    }
}
