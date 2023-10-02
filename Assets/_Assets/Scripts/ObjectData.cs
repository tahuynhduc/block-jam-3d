using System;
using UnityEngine;
using UnityEngine.Events;

public class ObjectData : Element<Transform, ElementType, ObjectData>
//MonoBehaviour
{
    #region private
    [SerializeField] ElementType _elementType;
    [SerializeField] bool _selectable;
    [SerializeField] GameObject[] _stateObj;
    GameBoardController gameBoard => FindObjectOfType<GameBoardController>();
    bool _objInQueueSecond;
    #endregion
    public ElementType Type { get => _elementType; set => _elementType = value; }
    public bool Selectable { get => _selectable; set => _selectable = value; }
    public bool ObjInQueueSecond { get => _objInQueueSecond; set => _objInQueueSecond = value; }

    public int label;
    private void Start()
    {
        label = GetVerticeLabel(Index);
    }

    private void OnMouseDown()
    {
        if (!Selectable)
            return;
        MoveToQueue();
    }
    private void MoveToQueue()
    {
        gameBoard.SetStateObjOnMatrix(this, ObjInQueueSecond);
    }
    public bool isMatching;
    public void DoMatching(bool state)
    {
        isMatching = state;
        gameObject.SetActive(!state);
    }
    public void ClickObjOnEnable(bool state)
    {
        GetComponent<Collider>().enabled = state;
    }
    public void SetState(bool selectable)
    {
        Selectable = selectable;
        _stateObj[0].SetActive(selectable);
        _stateObj[1].SetActive(!selectable);
    }
    private int GetVerticeLabel(MatrixIndex matrixIndex)
    {
        return matrixIndex.Row * Column + matrixIndex.Column;
    }

    public void SetTranform(Transform transMatrix)
    {
        transform.position = transMatrix.position;
    }
}