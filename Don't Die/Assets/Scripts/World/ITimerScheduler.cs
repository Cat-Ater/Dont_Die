using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITimerScheduler
{
    public string RegistrationString { get; }
    public float RegistrationTime { get; }
    public bool Notified { get; }

    public void Notification(); 
}
