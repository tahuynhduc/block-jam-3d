using System;
using System.Collections.Generic;

[Serializable]
public class WaitType
{
    public List<ObjectData> waitObject;
    public void SetTypeObj(ObjectData objectData)
    {
        waitObject.Add(objectData);
    }
    public void RemoveMatchingWaitLine(bool state)
    {
        for (int i = 0; i < waitObject.Count; i++)
        {
            waitObject[i].DoMatching(state);
        }
        waitObject.Clear();
    }
    public ObjectData FindObjOnWaitQueue(ObjectData objectData)
    {
        return waitObject.Find(x => x = objectData);
    }
    public void RemoveObjToWaitQueue(ObjectData objectData)
    {
        waitObject.Remove(objectData);
    }
}