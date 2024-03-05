using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for scheduling objects based on the games current time. 
/// </summary>
public class ObjectScheduler
{
    /// <summary>
    /// Private reference to the current game timer. 
    /// </summary>
    private Timer _gameTimer; 
    /// <summary>
    /// List containing the currently scheduled objects. 
    /// </summary>
    private List<ITimerScheduler> scheduledObjects;

    /// <summary>
    /// CTOR: 
    /// </summary>
    /// <param name="gameTimer"> The timer used to time the game. </param>
    public ObjectScheduler(Timer gameTimer)
    {
        _gameTimer = gameTimer; 
        scheduledObjects = new List<ITimerScheduler>();
    }

    /// <summary>
    /// Add a scheduled object to the scheduler. 
    /// </summary>
    /// <param name="sObject"> The interface to add. </param>
    public void AddScheduledObject(ITimerScheduler sObject)
    {
        _gameTimer.InsertTime(sObject.RegistrationString, sObject.RegistrationTime);
        scheduledObjects.Add(sObject);
    }

    /// <summary>
    /// Function used to update the scheduler. 
    /// </summary>
    public void UpdateObjects()
    {
        for (int i = 0; i < scheduledObjects.Count; i++)
        {
            if (_gameTimer.GetTimeElapsed(scheduledObjects[i].RegistrationString))
            {
                if (!scheduledObjects[i].Notified)
                    scheduledObjects[i].Notification();
            }
        }
    }
}
