using UnityEngine;

[System.Serializable]
public class PatternObject : PatternTimedEvent
{
    public GameObject prefab;

    public override void FireEvent()
    {
        fired = true;
        GameObject obj = GameObject.Instantiate(prefab);
    }
}