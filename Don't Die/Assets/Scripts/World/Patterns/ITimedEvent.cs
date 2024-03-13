public interface ITimedEvent
{
    public enum TimedEventType
    {
        START,
        END,
        TIMED,
        BUTTON_PRESS
    }
    public TimedEventType EventType { get; }

    public float Time { get; }

    public bool Fired { get; }

    public void FireEvent();
}
