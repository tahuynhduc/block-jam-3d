using System;

[Serializable]
public class RowMatrixReference 
{
    
    public ObjectData[] ObjectData;
    public ObjectData GetObjectReference(int colum)
    {
        return ObjectData[colum];
    }
}
