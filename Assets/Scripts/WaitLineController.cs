using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitLineController : MonoBehaviour
{
    // [SerializeField] 
    [SerializeField]
    List<WaitPositionData> _listWaitPosition;
    public List<WaitPositionData> WaitPosition { get => _listWaitPosition; set => _listWaitPosition = value; }
    public List<WaitType> WaitTypes { get => _waitTypes; set => _waitTypes = value; }

    [SerializeField]
    List<WaitType> _waitTypes;
    private void Reset()
    {
        WaitPosition = new List<WaitPositionData>(transform.GetComponentsInChildren<WaitPositionData>());
    }
    private WaitPositionData GetPositionData()
    {
        for (int i = 0; i < WaitPosition.Count; i++)
        {
            if (!WaitPosition[i]._isObj)
            {
                return WaitPosition[i];
            }
        }
        return null;
    }
    public void AddTolist(ObjectData objectData)
    {
        var positionData = GetPositionData();
        objectData.SetPositionData(positionData);
        if (!positionData._isObj)
            positionData._objectData = objectData;
        AddToWaitTypes(objectData);
    }

    private void AddToWaitTypes(ObjectData objectData)
    {
        for (int i = 0; i < WaitTypes.Count; i++)
        {
            if ((int)objectData.objectType == i)
            {
                WaitTypes[(int)objectData.objectType].waitObject.Add(objectData);
            }
        }
        CheckOutQueue();
    }

    public void CheckOutQueue()
    {
        for (int i = 0; i < WaitTypes.Count; i++)
        {
            if (IsMatchingLine(WaitTypes[i]))
            {
                RemoveMatchingWaitLine(WaitTypes[i]);
            }
        }
    }

    private void RemoveMatchingWaitLine(WaitType waitType)
    {
        for (var i = waitType.waitObject.Count - 1; i >= 0; i--)
        {
            var objectData = waitType.waitObject[i];
            var waitPosition = objectData.WaitPosition;
            waitPosition.Reset();
            objectData.DoMatching();
        }
        waitType.waitObject.Clear();
    }

    private bool IsMatchingLine(WaitType waitType)
    {
        return waitType.waitObject.Count > 2;
    }
}
[Serializable]
public class WaitType
{
    public List<ObjectData> waitObject;
}