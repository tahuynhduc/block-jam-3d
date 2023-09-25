using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class RowLogicMatrix
{
    public PositionObject[] PositionObjects;
    public PositionObject GetTypePosition(int colum)
    {
        return PositionObjects[colum];
    }
}