using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_TimerScheduler : ScheduledObject
{
    public GameObject spawnTest;
    public Vector2 spawnPosition;
    public float spawnTime = 1.0F;

    public override float RegistrationTime { get => spawnTime; }
    public override string RegistrationString { get => "Test_001"; }

    internal override void NotificationEvent()
    {
        GameObject.Instantiate(spawnTest, spawnPosition, Quaternion.identity);
    }
}

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
