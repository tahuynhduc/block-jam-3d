using System.Collections.Generic;
using UnityEngine;

public class PositionObject : MonoBehaviour
{
    [SerializeField] ObjectState _objectType;

    [SerializeField] ObjectData _objectData;
    public ObjectState objectState { get => _objectType; set => _objectType = value; }
    public ObjectData ObjectData { get => _objectData; set => _objectData = value; }
}

