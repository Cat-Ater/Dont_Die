using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pattern
{
    public string name;
    public int order;
    public bool complete = false;
    public float patternStart; 
    public float patternEndTime;

    public List<PatternObject> patternObjects;
    public List<PatternDialogue> patternDialogues;

    public void OnStart(float startTime)
    {
        patternStart = startTime; 
        FireType(ITimedEvent.TimedEventType.START);
    }

    public bool PatternCompletion(float time)
    {
        if (time >= patternStart + patternEndTime)
        {
            FireType(ITimedEvent.TimedEventType.END);
            return true;
        }
        return false;
    } 

    public void Update(float gameTime)
    {
        FireType(ITimedEvent.TimedEventType.TIMED, gameTime);
    }

    private void FireType(ITimedEvent.TimedEventType type) => FireType(type, 0);

    private void FireType(ITimedEvent.TimedEventType type, float time)
    {

        for (int i = 0; i < patternObjects.Count; i++)
        {
            if (patternObjects[i].CanFire(type, time))
            {
                patternObjects[i].FireEvent();
                Debug.Log("Firing =: " + patternObjects[i].name);
            }
        }

        for (int i = 0; i < patternDialogues.Count; i++)
        {
            if (patternDialogues[i].CanFire(type, time))
            {
                patternDialogues[i].FireEvent();
            }
        }
    }
}
