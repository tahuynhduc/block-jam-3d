using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
public class ObjectData : Element<Transform, ElementType, ObjectData>
//MonoBehaviour
{
    #region private
    [SerializeField] ElementType _elementType;
    WaitPositionData _waitPosition;
    TestGameBoard gameBoard;
    [SerializeField]
    private bool _selectable;
    public int ádasd;
    #endregion
    public ElementType Type { get => _elementType; }
    public WaitPositionData WaitPosition { get => _waitPosition; private set => _waitPosition = value; }
    public bool Selectable { get => _selectable; set => _selectable = value; }

    public int[,] test;
    [SerializeField] private List<Mesh> meshes;

    private void Awake()
    {
        gameBoard = FindObjectOfType<TestGameBoard>();
    }
    private void OnMouseDown()
    {
        if (!Selectable)
            return;
        MoveToQueue();

    }
    private void MoveToQueue()
    {
        gameBoard.SetStateObjOnMatrix(this);
    }
    public void DoMatching()
    {
        gameObject.SetActive(false);
    }
    public void SetPositionData(WaitPositionData positionData)
    {
        WaitPosition = positionData;
        transform.position = positionData.Position;
    }

    public void SetState(bool selectable)
    {
        Selectable = selectable;
        if (!selectable)
            ádasd = 1;
        else
            ádasd = 0;
        gameObject.GetComponent<MeshFilter>().mesh = meshes[ádasd];

    }

}