using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitLineController : MonoBehaviour
{
    #region private
    [SerializeField] List<WaitPositionData> _listWaitPosition;
    [SerializeField] List<WaitType> _waitTypes;
    #endregion
    #region property
    public List<WaitPositionData> WaitPosition { get => _listWaitPosition; set => _listWaitPosition = value; }
    public List<WaitType> WaitTypes { get => _waitTypes; set => _waitTypes = value; }
    #endregion
    private void Reset()
    {
        WaitPosition = new List<WaitPositionData>(GetComponentsInChildren<WaitPositionData>());
    }
    private WaitPositionData GetPositionData()
    {
        for (int i = 0; i < WaitPosition.Count; i++)
            if (!WaitPosition[i]._objectData)
            {
                return WaitPosition[i];
            }
        return null;
    }
    public void Add(ObjectData objectData)
    {
        var positionData = GetPositionData();
        objectData.SetPositionData(positionData);
        if (!positionData._objectData)
        {
            positionData._objectData = objectData;
            AddToWaitTypes(positionData._objectData);
        }
    }

    private void AddToWaitTypes(ObjectData objectData)
    {
        for (int i = 0; i < WaitTypes.Count; i++)
        {
            if (ObjectType(objectData) == i)
                WaitTypes[ObjectType(objectData)].waitObject.Add(objectData);
        }
        UpdatePositionObjectSameType();
        CheckOutQueue();
    }

    private int ObjectType(ObjectData objectData)
    {
        return (int)objectData.Type;
    }

    private void CheckOutQueue()
    {
        for (int i = 0; i < WaitTypes.Count; i++)
            if (IsMatchingLine(WaitTypes[i]))
                RemoveMatchingWaitLine(WaitTypes[i]);
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
        UpdatePositionInQueue();
    }
    private void UpdatePositionObjectSameType()
    {
        for (int i = 0; i < WaitPosition.Count; i++)
            if (WaitPosition[i]._objectData != null)
                for (int j = i; j < WaitPosition.Count; j++)
                    if (WaitPosition[j]._objectData != null)
                        if (WaitPosition[j]._objectData.Type == WaitPosition[i]._objectData.Type && j > i)
                        {
                            var swapPosition = WaitPosition[i += 1]._objectData;
                            WaitPosition[i]._objectData = WaitPosition[j]._objectData;
                            WaitPosition[j]._objectData = swapPosition;
                            WaitPosition[i]._objectData.SetPositionData(WaitPosition[i]);
                            WaitPosition[j]._objectData.SetPositionData(WaitPosition[j]);
                        }

    }
    private void UpdatePositionInQueue()
    {
        for (int i = 0; i < WaitPosition.Count; i++)
            if (WaitPosition[i]._objectData != null)
                for (int j = 0; j < i; j++)
                    if (WaitPosition[j]._objectData == null)
                    {
                        WaitPosition[j]._objectData = WaitPosition[i]._objectData;
                        WaitPosition[j]._objectData.SetPositionData(WaitPosition[j]);
                        WaitPosition[i].Reset();
                        break;
                    }
    }
    private bool IsMatchingLine(WaitType waitType)
    {
        return waitType.waitObject.Count > 2;
    }
}
