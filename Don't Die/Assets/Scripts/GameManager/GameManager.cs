using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for handling updating the game and acting as a system inbetween. 
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// The current static instance of the GameManager. 
    /// </summary>
    private static GameManager _instance;

    /// <summary>
    /// The current instance of the game timer. 
    /// </summary>
    private Timer _gameTimer;

    /// <summary>
    /// Reference to the ObjectScheduler. 
    /// </summary>
    private ObjectScheduler _objectScheduler; 

    /// <summary>
    /// Returns the current GameManager. 
    /// </summary>
    public static GameManager Instance => _instance;

    /// <summary>
    /// Returns the current GameTimer instance. 
    /// </summary>
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

        //Set up object scheduling. 
        _objectScheduler = new ObjectScheduler(_gameTimer); 

        //Load the UI into the scene if not present.
        SceneManager.LoadSceneAsync("_UI", LoadSceneMode.Additive); 
    }

    void Update()
    {
        //Update system objects here. 
        _gameTimer.UpdateTimer(Time.deltaTime);
        _objectScheduler.UpdateObjects();
    }

    /// <summary>
    /// Call to add an object to scheduling. 
    /// </summary>
    /// <param name="obj"></param>
    public void ScheduleObject(ITimerScheduler obj) => _objectScheduler.AddScheduledObject(obj);
}
