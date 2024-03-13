using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PatternTimedEvent : ITimedEvent
{
    public string name;
    internal bool fired = false;

    [Range(0, 60)]
    public float time;

    public ITimedEvent.TimedEventType eventType;

    ITimedEvent.TimedEventType ITimedEvent.EventType => eventType;
    public float Time => time;
    public bool Fired => fired;

    public int EventTypeID => (int)eventType;

    public abstract void FireEvent();

    public bool CanFire(ITimedEvent.TimedEventType type, float gameTime)
    {
        bool fire;
        switch (type)
        {
            case ITimedEvent.TimedEventType.START:
                fire = (fired == false && type == this.eventType);
                break;
            case ITimedEvent.TimedEventType.END:
                fire = (fired == false && type == this.eventType);
                break;
            case ITimedEvent.TimedEventType.TIMED:
                fire = (gameTime >= time && Fired  == false);
                break;
            case ITimedEvent.TimedEventType.BUTTON_PRESS:
                fire = (fired == false && type == this.eventType);
                break;
            default:
                return false;
        }

        return fire;
    }
}

[System.Serializable]
public class Pattern
{
    public string name;
    public int order;
    public bool complete = false;
    public float patternEndTime;

    public List<PatternObject> patternObjects;
    public List<PatternDialogue> patternDialogues;

    public void OnStart() => FireType(ITimedEvent.TimedEventType.START);

    public bool PatternCompletion(float time)
    {
        if (time >= patternEndTime)
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
