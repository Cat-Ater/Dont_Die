using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple test class for testing the scheduler. 
/// </summary>
public class Test_TimerScheduler : ScheduledObject
{
    /// <summary>
    /// The object to spawn at point of notification. 
    /// </summary>
    public GameObject spawnTest;

    /// <summary>
    /// The position to spawn the object at. 
    /// </summary>
    public Vector2 spawnPosition;

    /// <summary>
    /// The time at which to spawn the object. 
    /// </summary>
    public float spawnTime = 1.0F;

    /// <summary>
    /// Override function responsible for retriving the spawnTime. 
    /// </summary>
    public override float RegistrationTime { get => spawnTime; }

    /// <summary>
    /// Override function responsible for retriving the string used to identify the object.
    /// </summary>
    public override string RegistrationString { get => "Test_001"; }

    /// <summary>
    /// Function called to notify that the schedule has elapsed. 
    /// </summary>
    internal override void NotificationEvent()
    {
        GameObject.Instantiate(spawnTest, spawnPosition, Quaternion.identity);
    }
}
