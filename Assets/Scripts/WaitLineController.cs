using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitLineController : MonoBehaviour
{
    // [SerializeField] 
    [SerializeField]
    List<WaitPositionData> _listWaitPosition;
    [SerializeField]
    List<WaitType> _waitTypes;
    public List<WaitPositionData> WaitPosition { get => _listWaitPosition; set => _listWaitPosition = value; }
    public List<WaitType> WaitTypes { get => _waitTypes; set => _waitTypes = value; }
    private void Reset()
    {
        WaitPosition = new List<WaitPositionData>(GetComponentsInChildren<WaitPositionData>());
    }
    private WaitPositionData GetPositionData()
    {
        for (int i = 0; i < WaitPosition.Count; i++)
        {
            if (!WaitPosition[i]._objectData)
            {
                return WaitPosition[i];
            }
        }
        return null;
    }
    public void AddTolist(ObjectData objectData)
    {
        //lấy vị trí position trong list wait
        var positionData = GetPositionData();
        //gán wait position trong objectData là wait position trong list
        objectData.SetPositionData(positionData);
        //kiểm tra object trong wait position có null không
        if (!positionData._objectData)
        {
            //nếu null thì gán object trong wait position là objectData
            positionData._objectData = objectData;
            //add object trong wait postion vào list
            AddToWaitTypes(positionData._objectData);
        }
        //UpdatePositionObjectSameType(objectData);
    }

    private void AddToWaitTypes(ObjectData objectData)
    {
        for (int i = 0; i < WaitTypes.Count; i++)
        {
            //kiểm tra type of objectData trong wait postion là loại nào thì đưa vào list tương ứng
            if (ObjectType(objectData) == i)
            {
                WaitTypes[ObjectType(objectData)].waitObject.Add(objectData);
            }
        }
        UpdatePositionObjectSameType(objectData);
        CheckOutQueue();
    }

    private int ObjectType(ObjectData objectData)
    {
        return (int)objectData.objectType;
    }

    private void CheckOutQueue()
    {
        for (int i = 0; i < WaitTypes.Count; i++)
        {
            // kiểm tra type of object nhiều hơn 2 thì remove list chứa type of object tương ứng
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
            //reset position của objectData và detroy chính nó
        }
        waitType.waitObject.Clear();
        UpdatePositionInQueue();
    }
    private void UpdatePositionObjectSameType(ObjectData objectData)
    {
        for (int i = 0; i < WaitPosition.Count; i++)
        {
            if (WaitPosition[i]._objectData != null)
                for (int j = i; j < WaitPosition.Count; j++)
                {
                    if (WaitPosition[j]._objectData != null)
                        if (WaitPosition[j]._objectData.objectType == WaitPosition[i]._objectData.objectType && j > i)
                        {
                            var swapPosition = WaitPosition[i += 1]._objectData;
                            WaitPosition[i]._objectData = WaitPosition[j]._objectData;
                            WaitPosition[j]._objectData = swapPosition;
                            WaitPosition[i]._objectData.SetPositionData(WaitPosition[i]);
                            WaitPosition[j]._objectData.SetPositionData(WaitPosition[j]);
                        }

                }
        }
    }
    private void UpdatePositionInQueue()
    {
        for (int i = 0; i < WaitPosition.Count; i++)
        {
            if (WaitPosition[i]._objectData != null)
            {
                for (int j = 0; j < i; j++)
                {
                    if (WaitPosition[j]._objectData == null)
                    {
                        WaitPosition[j]._objectData = WaitPosition[i]._objectData;
                        //WaitPosition[j]._objectData.transform.position = WaitPosition[j].transform.position;
                        WaitPosition[j]._objectData.SetPositionData(WaitPosition[j]);
                        WaitPosition[i].Reset();
                        break;
                    }
                }
            }
        }
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