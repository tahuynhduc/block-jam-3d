using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListPosition : MonoBehaviour
{
    [SerializeField] List<PositionObject> _positionObject;
    private void Reset()
    {
        _positionObject = new List<PositionObject>(transform.GetComponentsInChildren<PositionObject>());
    }
    private void OnValidate()
    {
        Reset();
    }
}
