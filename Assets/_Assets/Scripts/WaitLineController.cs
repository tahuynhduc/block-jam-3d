using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaitLineController : GameBoard<Transform, TypeWaitQueue, ObjectData>
{
    #region private
    [SerializeField] List<WaitType> _waitTypes;
    [SerializeField] List<ObjectData> _waitQueueSecond = new List<ObjectData>();
    [SerializeField] List<ObjectData> _waitQueueFirst = new List<ObjectData>();
    [SerializeField] InterstitialAdExample _interstitialAdExample;
    [SerializeField] UIGameController _uIGameController;
    int _lengthSecondWaitQueue;
    #endregion
    private void Awake()
    {
        CreateMaxtrix();
        _interstitialAdExample = FindObjectOfType<InterstitialAdExample>();
    }
    private void OnEnable()
    {
        _interstitialAdExample?.LoadAd();
    }
    #region Item

    private void SetObjInWaitQueue(ObjectData obj)
    {
        if (!_waitQueueSecond.Contains(obj))
            _waitQueueSecond.Add(obj);

        var objData = _waitTypes[(int)obj.Type].FindObjOnWaitQueue(obj);
        _waitTypes[(int)obj.Type].RemoveObjToWaitQueue(objData);
    }
    public void MoveObjToSecondQueue(bool state)
    {
        var count = 0;
        if (WaitQueueSeconIsOnEnable())
            return;
        for (int i = _waitQueueFirst.Count - 1; i >= 0; i--)
        {
            if (_waitQueueFirst[i] && count <= 2)
            {
                count++;
                SetObjInWaitQueue(_waitQueueFirst[i]);
                _waitQueueFirst[i] = null;
            }
        }
        UpdateNewPosition(state);
    }

    public bool WaitQueueSeconIsOnEnable()
    {
        return _waitQueueSecond.Count > 0;
    }

    private void UpdateNewPosition(bool state)
    {
        _waitQueueSecond.Reverse();
        for (int i = 0; i < _waitQueueSecond.Count; i++)
        {
            Debug.Log("Test");
            var newIndex = At<Transform>((int)TypeWaitQueue.ReserveWaitQueue, i);
            _waitQueueSecond[i].SetTranform(newIndex);
            var stateObj = _waitQueueSecond[i].GetState();
            _waitQueueSecond[i].ClickObjOnEnable(stateObj);
            _waitQueueSecond[i].ObjInQueueSecond = true;
        }
    }
    public void UpdatePositionInWaitQueue(ObjectData objectData)
    {
        for (int i = 0; i < _waitQueueFirst.Count; i++)
        {
            if (_waitQueueFirst[i].Label == objectData.Label)
            {
                _waitQueueFirst[i] = null;
                var objtype = _waitTypes[(int)objectData.Type].FindObjOnWaitQueue(objectData);
                _waitTypes[(int)objectData.Type].RemoveObjToWaitQueue(objtype);
                UpdatePositionInQueue();
                break;
            }
        }
    }
    #endregion
    public void Add(ObjectData objectData)
    {
        if (_waitQueueSecond.Contains(objectData))
            _waitQueueSecond.Remove(objectData);
        for (int j = 0; j < Column; j++)
        {
            if (_waitQueueFirst[j] == null)
            {
                _waitQueueFirst[j] = objectData;
                var index = At<Transform>((int)TypeWaitQueue.MainQueue, j);
                objectData.transform.position = index.position;
                AddToWaitTypes(objectData);
                return;
            }
        }
    }

    public bool CheckGameOver()
    {
        return (_waitQueueFirst[_waitQueueFirst.Count - 1] != null);
    }
    private void AddToWaitTypes(ObjectData objectData)
    {
        for (int i = 0; i < _waitTypes.Count; i++)
        {
            if (ObjectType(objectData) == i)
                _waitTypes[ObjectType(objectData)].SetTypeObj(objectData);
        }
        UpdatePositionObjectSameType(objectData);
        CheckOutQueue();
        UpdatePositionInQueue();
        if (CheckGameOver())
        {
            _uIGameController.ShowGameOver();
            _interstitialAdExample.ShowAd();
        }
    }

    private int ObjectType(ObjectData objectData)
    {
        return (int)objectData.Type;
    }

    private void CheckOutQueue()
    {
        for (int i = 0; i < _waitTypes.Count; i++)
            if (IsMatchingLine(_waitTypes[i]))
            {
                _waitTypes[i].RemoveMatchingWaitLine(IsMatchingLine(_waitTypes[i]));
                ClearWaitQueue();
            }
    }
    private void ClearWaitQueue()
    {
        for (int i = 0; i < _waitQueueFirst.Count; i++)
        {
            if (_waitQueueFirst[i] == null)
                return;
            if (_waitQueueFirst[i].isMatching)
            {
                _waitQueueFirst[i] = null;
            }
        }
    }
    private void UpdatePositionObjectSameType(ObjectData objData)
    {
        for (int i = 0; i < _waitQueueFirst.Count; i++)
        {
            for (int j = i; j < _waitQueueFirst.Count; j++)
                if (_waitQueueFirst[j] != null)
                    if (_waitQueueFirst[j].Type == _waitQueueFirst[i].Type && j > i)
                    {
                        var swapPosition = _waitQueueFirst[i += 1];
                        _waitQueueFirst[i] = _waitQueueFirst[j];
                        _waitQueueFirst[j] = swapPosition;
                        var index = At<Transform>((int)TypeWaitQueue.MainQueue, j);
                        var currentIndex = At<Transform>((int)TypeWaitQueue.MainQueue, i);
                        _waitQueueFirst[i].SetTranform(currentIndex);
                        _waitQueueFirst[j].SetTranform(index);
                    }
        }
    }
    private void UpdatePositionInQueue()
    {
        for (int i = 0; i < _waitQueueFirst.Count; i++)
        {
            if (_waitQueueFirst[i] == null)
            {
                for (int j = i; j < _waitQueueFirst.Count; j++)
                {
                    if (_waitQueueFirst[j] != null)
                    {
                        _waitQueueFirst[i] = _waitQueueFirst[j];
                        var index = At<Transform>((int)TypeWaitQueue.MainQueue, i);
                        _waitQueueFirst[j].SetTranform(index);
                        _waitQueueFirst[j] = null;
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
public enum TypeWaitQueue
{
    MainQueue,
    ReserveWaitQueue,
}