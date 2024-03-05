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
