using System;
using UnityEngine;

[Serializable]
public class PositionMatrix
{
    [SerializeField]
    public PositionRow[] _positionRows;

    public Vector3 At(int row, int col)
    {
        return _positionRows[row].At(col);
    }
}
