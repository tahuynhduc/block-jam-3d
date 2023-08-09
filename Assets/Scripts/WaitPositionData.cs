using UnityEngine;

public class WaitPositionData : MonoBehaviour
{
    public bool _isObj => _objectData;
    public ObjectData _objectData;


    public void Reset()
    {
        _objectData = null;
    }
    public Vector3 Position => transform.position;

}
