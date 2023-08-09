using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectData : MonoBehaviour
{

    [SerializeField] ObjectType _objectType;
    WaitLineController _listWait;
    WaitPositionData _waitPosition;
    public ObjectType objectType { get => _objectType; }
    public WaitPositionData WaitPosition { get => _waitPosition; private set => _waitPosition = value; }

    private void Start()
    {
        _listWait = FindObjectOfType<WaitLineController>();
    }
    private void OnMouseDown()
    {
        MoveToQueue();
    }

    public void SetPositionData(WaitPositionData positionData)
    {
        WaitPosition = positionData;
        transform.position = positionData.Position;
        gameObject.GetComponent<Collider>().enabled = false;
    }

    private void MoveToQueue()
    {
        _listWait.AddTolist(this);
    }
    public void DoMatching()
    {
        Destroy(gameObject);
    }

}