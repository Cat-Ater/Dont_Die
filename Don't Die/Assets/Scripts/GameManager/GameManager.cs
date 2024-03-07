using Core.Systems;
using System;
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

    private DataHandler dHandler;
    private ConsumableHandler cHandler; 

    /// <summary>
    /// The current instance of the game timer. 
    /// </summary>
    private Timer _gameTimer;

    /// <summary>
    /// Reference to the ObjectScheduler. 
    /// </summary>
    private ObjectScheduler _objectScheduler;

    [SerializeField]
    private List<Vector2> bodyPositions;
    public static List<Vector2> BodyPositions; 

    /// <summary>
    /// Returns the current GameManager. 
    /// </summary>
    public static GameManager Instance => _instance;

    /// <summary>
    /// Returns the current GameTimer instance. 
    /// </summary>
    public static Timer GameTimer => _instance._gameTimer;

    /// <summary>
    /// Add a IConsumableDestruction interface to the system. 
    /// </summary>
    public static IConsumableDestruction AddConsumableDestruction 
    {
        set => ConsumableHandler.AddConsumableDestruction = value; 
    }

    /// <summary>
    /// Remove an IConsumableDestruction interface from the system. 
    /// </summary>
    public static IConsumableDestruction RemoveConsumableDestruction
    {
        set => ConsumableHandler.RemoveConsumableDestruction = value;
    }

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

        //Set up player data. 
        dHandler = DataHandler.CreateDataHandler();
        
        //Do body position handling. 
        if (bodyPositions == null)
            bodyPositions = new List<Vector2>(); 
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

    public void CollisionHandling()
    {
        Debug.Log("Player collided, RESET.");
    }

    /// <summary>
    /// Add a position at which the player died. 
    /// </summary>
    /// <param name="position"> The position to add. </param>
    public void SetDeathPosition(Vector2 position)
    {
        //Update data. 
        dHandler.data.totalNumberOfDeaths += 1;
        dHandler.data.lastAttemptLength = _gameTimer.Time;
        if (_gameTimer.Time > dHandler.data.longestTimeSurvived)
            dHandler.data.longestTimeSurvived = _gameTimer.Time;
        bodyPositions.Add(position);
    }

    public void ResetGame()
    {
        _gameTimer.ResetTimer(); 
    }

    /// <summary>
    /// Call to Destroy all IConsumableDestructions interfaces held in the system. 
    /// </summary>
    public void ConsumableDestruction() => ConsumableHandler.ConsumableDestruction();
}
