using System;
using UnityEngine;

[Serializable]
public class PositionRow
{
    [SerializeField]
    public Transform[] _positions;

    public Vector3 At(int column)
    {
        return _positions[column].position;
    }
}
