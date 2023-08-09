using System.Collections.Generic;
using UnityEngine;

public class PositionObject : MonoBehaviour
{
    [SerializeField] List<ObjectData> _obj;
    private void Start()
    {
        int index = Random.Range(0, _obj.Count);
        Instantiate(_obj[index], transform.position, Quaternion.identity);
    }
    private void OnValidate()
    {
        Reset();
    }
    private void Reset()
    {
        // _obj = new List<ObjectData>(transform.GetComponentsInChildren<ObjectData>());
    }
}
