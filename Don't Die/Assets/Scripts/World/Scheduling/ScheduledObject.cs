using UnityEngine;

public abstract class ScheduledObject : MonoBehaviour, ITimerScheduler
{
    public abstract float RegistrationTime { get; }
    public abstract string RegistrationString { get; }
    private bool NotifiedState { get; set; } = false;

    internal void Start()
    {
        GameManager.Instance.ScheduleObject(this);
    }

    string ITimerScheduler.RegistrationString => RegistrationString;

    float ITimerScheduler.RegistrationTime => RegistrationTime;

    bool ITimerScheduler.Notified => NotifiedState;

    void ITimerScheduler.Notification()
    {
        NotifiedState = true;
        NotificationEvent(); 
    }

    internal abstract void NotificationEvent();
}
