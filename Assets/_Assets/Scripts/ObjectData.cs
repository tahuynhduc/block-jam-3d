using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectData : Element<Transform, ElementType, ObjectData>
//MonoBehaviour
{
    #region private
    [SerializeField] ElementType _elementType;
    [SerializeField] bool selected;
    [SerializeField] GameObject[] _stateObj;
    [SerializeField] Animator _animator;
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
        SetAnimation("State", 1);
        ClickObjOnEnable(!selected);
        SetIndexElementType(ElementType.None);
        gameBoard.SetStateObjOnMatrix(this, ObjInQueueSecond);
    }
    public void SetAnimation(string type, int state)
    {
        _animator.SetInteger(type, state);
    }
    public void DoMatching(bool state)
    {
        isMatching = state;
        gameObject.SetActive(!state);
    }
    [SerializeField] MatrixIndex currentIndex;
    [SerializeField] List<Quaternion> quaternions;
    public void UpdateObjRotation(MatrixIndex matrix)
    {
        SetDirection(matrix);
    }
    private void SetDirection(MatrixIndex matrixIndex)
    {
        var index = 0;
        if (CheckCondition(matrixIndex, ref index) || currentIndex.Row == 0 || currentIndex.Column == 0)
        {
            transform.rotation = quaternions[index];
            currentIndex = matrixIndex;
        }
    }
    private bool CheckCondition(MatrixIndex matrix, ref int direction)
    {
        if (matrix.Row < currentIndex.Row)
        {
            direction = 1;
            return matrix.Row < currentIndex.Row;
        }
        if (matrix.Row > currentIndex.Row)
        {
            direction = 0;
            return matrix.Row > currentIndex.Row;
        }
        if (matrix.Column > currentIndex.Column)
        {
            direction = 3;
            return matrix.Column > currentIndex.Column;
        }
        if (matrix.Column < currentIndex.Column)
        {
            direction = 2;
            return matrix.Column < currentIndex.Column;
        }
        return false;
    }
    public void SetNewTranformPosition(Transform newTrans)
    {
        transform.position = newTrans.position;
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