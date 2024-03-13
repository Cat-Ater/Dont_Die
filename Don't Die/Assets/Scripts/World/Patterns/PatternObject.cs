using UnityEngine;

[System.Serializable]
public class PatternObject : PatternTimedEvent
{
    public GameObject prefab;

    public override void FireEvent()
    {
        fired = true;
        GameObject obj = GameObject.Instantiate(prefab);
#if DEBUG
        Debug.Log("Spawn Event Called: " + prefab.name);
#endif
    }
}