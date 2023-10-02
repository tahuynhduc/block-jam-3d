using UnityEngine;

public class WaitPositionData : MonoBehaviour
{
    public ObjectData _objectData;
    public void Reset()
    {
        _objectData = null;
    }
    public void Setstate(bool state)
    {
        gameObject.SetActive(state);
    }
    public Vector3 Position => transform.position;
}
