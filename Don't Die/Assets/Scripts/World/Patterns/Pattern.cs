using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimedEvent
{
    public enum TimedEventType
    {
        START,
        END,
        TIMED,
        BUTTON_PRESS,
        DEATH
    }

    public TimedEventType EventType { get; }

    public float Time { get; }

    public bool Fired { get; }

    public void FireOnStart() => FireEvent();

    public void FireOnDeath() => FireEvent();

    public void FireOnEnd() => FireEvent();

    public void FireEvent();
}

public abstract class PatternTimedEvent : ITimedEvent
{
    internal bool fired = false;

    [Range(0, 60)]
    public float time;

    public ITimedEvent.TimedEventType eventType;
    public ITimedEvent.TimedEventType EventType => eventType;
    public float Time => time;
    public bool Fired => fired;

    public abstract void FireEvent();
}

[System.Serializable]
public class PatternObject : PatternTimedEvent
{
    public GameObject prefab;
    public Vector2 position;

    public override void FireEvent()
    {
        GameObject.Instantiate(prefab, position, Quaternion.identity);
        fired = true;
    }
}

[System.Serializable]
public class PatternDialogue : PatternTimedEvent, ITextCaller
{
    public string[] dialogueStrings;
    public float scrollSpeed;
    public float endLineDelay;
    public float eventTime;

    public override void FireEvent() => DisplayText();

    private void DisplayText()
    {
        UIManager.Instance.DisplayTest(this, dialogueStrings, scrollSpeed, endLineDelay);
        fired = true;
    }

    public void DisplayComplete()
    {
    }
}

[System.Serializable]
public class Pattern
{
    //Responsiblilities: 
    // -> Contains a list of objects and times which they should be spawwned. 
    // -> Handles updating this list based on the current game time. 
    // -> Contains a flag denoting completion. 

    public string name;
    public int order;
    public bool complete = false;
    float length; 

    public List<PatternObject> patternObjects;
    public List<PatternDialogue> patternDialogues;

    public void OnStart()
    {
        FireType(ITimedEvent.TimedEventType.START);
    }

    public void OnDeath()
    {
        FireType(ITimedEvent.TimedEventType.DEATH);
    }

    public void OnEnd()
    {
        FireType(ITimedEvent.TimedEventType.END);
    }

    private void FireType(ITimedEvent.TimedEventType type)
    {
        if(type == ITimedEvent.TimedEventType.TIMED)
        {
            Debug.Log("Try Alt Func: <Insert Here>");
            return;
        }
            
        for (int i = 0; i < patternObjects.Count; i++)
        {
            if (patternObjects[i].eventType == type)
                patternObjects[i].FireEvent();
        }

        for (int i = 0; i < patternDialogues.Count; i++)
        {
            if (patternDialogues[i].eventType == type)
                patternDialogues[i].FireEvent();
        }
    }

    public void OnTime(float time)
    {
        if(time > length)
        {
            OnEnd(); 
        }

        for (int i = 0; i < patternDialogues.Count; i++)
        {
            if(patternDialogues[i].time <= time)
            {
                patternDialogues[i].FireEvent();
            }
        }

        for (int i = 0; i < patternObjects.Count; i++)
        {
            if (patternObjects[i].time <= time)
            {
                patternObjects[i].FireEvent();
            }
        }
    }
}
