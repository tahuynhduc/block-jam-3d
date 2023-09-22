using System;
using System.Collections.Generic;

[Serializable]
public class LogicMatrix 
{
    public RowLogicMatrix[] RowMatrixLogic;
    public PositionObject ReferenceMatrix(int row, int col)
    {
        return RowMatrixLogic[row].GetTypePosition(col);
    }
}
