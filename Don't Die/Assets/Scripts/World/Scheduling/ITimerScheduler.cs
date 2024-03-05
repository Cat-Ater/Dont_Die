using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interface used to provide calls to an object upon timer completion.
/// </summary>
public interface ITimerScheduler
{
    /// <summary>
    /// The string that the object should be registered under. 
    /// </summary>
    public string RegistrationString { get; }

    /// <summary>
    /// The time at which the object should be called. 
    /// </summary>
    public float RegistrationTime { get; }

    /// <summary>
    /// Whether this object has already been called by the system. 
    /// </summary>
    public bool Notified { get; }

    /// <summary>
    /// The function used to pass notification calls to the interfaced object. 
    /// </summary>
    public void Notification(); 
}
