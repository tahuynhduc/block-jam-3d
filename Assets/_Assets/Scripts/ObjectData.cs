using System;
using UnityEngine;
using UnityEngine.Events;

public class ObjectData : Element<Transform, ElementType, ObjectData>
//MonoBehaviour
{
    #region private
    [SerializeField] ElementType _elementType;
    [SerializeField] bool selected;
    [SerializeField] GameObject[] _stateObj;
    GameBoardController gameBoard => FindObjectOfType<GameBoardController>();
    private int _label;
    #endregion
    #region public
    public ElementType Type { get => _elementType; set => _elementType = value; }
    public int Label { get => _label; }
    public bool ObjInQueueSecond;
    public bool isMatching;
    #endregion
    private void Start()
    {
        _label = GetVerticeLabel(Index);
    }
    private void OnMouseDown()
    {
        if (!selected)
            return;
        MoveToQueue();
    }
    private void MoveToQueue()
    {
        ClickObjOnEnable(!selected);
        SetIndexElementType(ElementType.None);
        gameBoard.SetStateObjOnMatrix(this, ObjInQueueSecond);
    }
    public void DoMatching(bool state)
    {
        isMatching = state;
        gameObject.SetActive(!state);
    }
    public void ClickObjOnEnable(bool state)
    {
        GetComponent<Collider>().enabled = state;
    }
    public bool GetState()
    {
        return selected;
    }
    public void SetState(bool selectable)
    {
        selected = selectable;
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