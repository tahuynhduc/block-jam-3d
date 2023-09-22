using System;
using UnityEngine;

[Serializable]
public class MatrixIndex
{
    [SerializeField] public Vector2Int _index;

    public MatrixIndex(Vector2Int index)
    {
        _index = index;
    }

    public MatrixIndex(int row, int column)
    {
        _index = new Vector2Int(column, row);
    }

    public Vector2Int Index => _index;
    public int Row => Index.y;
    public int Column => Index.x;

    public override string ToString()
    {
        return _index.ToString();
    }
}
