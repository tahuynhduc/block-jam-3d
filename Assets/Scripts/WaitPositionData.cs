using UnityEngine;

public class WaitPositionData : MonoBehaviour
{
    public ObjectData _objectData;
    public void Reset()
    {
        Debug.Log("reset:" + gameObject.name);
        _objectData = null;
    }
    public Vector3 Position => transform.position;

}
