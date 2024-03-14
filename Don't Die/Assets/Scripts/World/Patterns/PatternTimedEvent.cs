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
