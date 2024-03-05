using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public Timer _gameTimer;

    private List<ITimerScheduler> scheduledObjects;

    public static GameManager Instance => _instance;
    public static Timer GameTimer => _instance._gameTimer;


    void Awake()
    {

        //Create the singleton. 
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }

        //Set up timer
        _gameTimer = new Timer();
        _gameTimer.InsertTime("Game Completed", 60);

        //Load the UI into the scene if not present.
        SceneManager.LoadSceneAsync("_UI", LoadSceneMode.Additive);

        //Generate scheduler. 
        scheduledObjects = new List<ITimerScheduler>(); 
    }

    void Update()
    {
        _gameTimer.UpdateTimer(Time.deltaTime);
        
        for (int i = 0; i < scheduledObjects.Count; i++)
        {
            if (_gameTimer.GetTimeElapsed(scheduledObjects[i].RegistrationString))
            {
                if(!scheduledObjects[i].Notified)
                    scheduledObjects[i].Notification();
            }
        }
    }

    public void ScheduleObject(ITimerScheduler obj)
    {
        _gameTimer.InsertTime(obj.RegistrationString, obj.RegistrationTime);
        scheduledObjects.Add(obj);
    }
}
