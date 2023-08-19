using System.Collections.Generic;
using UnityEngine;

public class MatrixPosition : MonoBehaviour
{
    [SerializeField] List<ListPosition> _maxtrixPosition;
    private void Reset()
    {
        _maxtrixPosition = new List<ListPosition>(transform.GetComponentsInChildren<ListPosition>());
    }
    private void OnValidate()
    {
        Reset();
    }

    private void Start()
    {
        //foreach(var test in _maxtrixPosition)
        //{
        //    //Debug.Log("test:"+test);
        //    //for(var check in test.)
        //}
    }
}