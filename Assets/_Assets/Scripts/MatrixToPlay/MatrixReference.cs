using System;

[Serializable]
public class MatrixReference
{
    public RowMatrixReference[] RowMatrixReference;
    public ObjectData MatrixReferenceObject(int row, int col)
    {
        return RowMatrixReference[row].GetObjectReference(col);
    }
}