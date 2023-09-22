using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class BoardManager : MonoBehaviour
{
    [SerializeField] PositionMatrix _positionMatrix;
    [SerializeField] LogicMatrix _logicMatrix;
    [SerializeField] MatrixReference _matrixReference;

    [SerializeField] ObjectData[] _objPrefab;
    [SerializeField] int _numberObj;
    [SerializeField] List<ObjectData> _arrayObjData;
    private int Row => _logicMatrix.RowMatrixLogic.Length;
    private int Colum => _logicMatrix.RowMatrixLogic[0].PositionObjects.Length;
    void Start()
    {
        CreateObjects();
    }

    public IEnumerator UnlockObject()
    {
        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Colum; j++)
            {
                var getType = GetPositionType(i, j);
                if (getType.objectState == ObjectState.None)
                {
                    Debug.Log("chech" + i + "../" + j);
                    var getup = GetPositionType(i - 1, j);
                    getup.objectState = ObjectState.None;
                    //getup.ObjectData.SetStateObj(getup.objectState == ObjectState.None);
                }
            }
        }
        yield return null;
    }
    //private void CreateObjects()
    //{
    //    for (int i = 0; i < _positionMatrix._positionRows.Length; i++)
    //    {
    //        for (int j = 0; j < _positionMatrix._positionRows[i]._positions.Length; j++)
    //        {
    //            var position = GetPosition(i, j);
    //            var createObjects = GetPositionType(i, j);
    //            var GetRandomIndex = Random.Range(0, _lenghObj.Count);
    //            createObjects.ObjectData = Instantiate(_lenghObj[GetRandomIndex], position, Quaternion.identity);
    //            _lenghObj.RemoveAt(GetRandomIndex);
    //            if (createObjects.objectState == ObjectState.Block)
    //            {
    //                createObjects.ObjectData.SetStateObj(createObjects.objectState == ObjectState.None);
    //            }
    //        }
    //    }
    //    StartCoroutine(UnlockObject());
    //}
    private void CreateObjects()
    {
        //for (int i = 0; i < _matrixReference.RowMatrixReference.Length; i++)
        //{
        //    for (int j = 0; j < _matrixReference.RowMatrixReference[i].ObjectData.Length; j++)
        //    {
        //        var position = GetPosition(i, j);
        //        var obj = _matrixReference.RowMatrixReference[i].ObjectData;
        //        obj[j] = Instantiate(_objPrefab, position, Quaternion.identity);
        //    }
        //}
        //CreateObjects();
    }
    private Vector3 GetPosition(int row, int col)
    {
        return _positionMatrix.At(row, col);
    }
    private PositionObject GetPositionType(int row, int col)
    {
        return _logicMatrix.ReferenceMatrix(row, col);
    }
    private ObjectData GetObject(int row, int col)
    {
        return _matrixReference.MatrixReferenceObject(row, col);
    }
}
