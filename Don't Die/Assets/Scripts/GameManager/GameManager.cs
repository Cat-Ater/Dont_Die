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
    public PlayerRespawner respawner;

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
    public GameObject splatterPrefab;
    public GameObject[] deadBodyPrefabs;

    public Vector2 rangeOffset = new Vector2(-1.5F, 1.5F);

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

    public static bool PlayerRespawnable => _instance.respawner != null;

    void Awake()
    {

        //Create the singleton. 
        if (_instance == null)
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

    #region Object Scheduling. 
    /// <summary>
    /// Call to add an object to scheduling. 
    /// </summary>
    /// <param name="obj"></param>
    public void ScheduleObject(ITimerScheduler obj) => _objectScheduler.AddScheduledObject(obj);
    #endregion

    /// <summary>
    /// Add a position at which the player died. 
    /// </summary>
    /// <param name="position"> The position to add. </param>
    public void SetDeathPosition(Vector2 position)
    {
        //Update data. 
        DataHandler.UpdateData(GameManager.GameTimer);
        bodyPositions.Add(position);
        GameObject.Instantiate(splatterPrefab, PositionOffset(position), Quaternion.identity);
        GameObject.Instantiate(SelectRandomPrefab(deadBodyPrefabs), position, Quaternion.identity);
    }

    public void ResetGame()
    {
        _gameTimer.ResetTimer();
    }

    /// <summary>
    /// Call to Destroy all IConsumableDestructions interfaces held in the system. 
    /// </summary>
    public void ConsumableDestruction() => ConsumableHandler.ConsumableDestruction();

    #region Helper Funcs.
    public GameObject SelectRandomPrefab(GameObject[] objects)
    {
        int _a;
        int _b;
        GameObject temp;

        GameObject[] arr = objects;

        for (int i = 0; i < 20; i++)
        {
            _a = UnityEngine.Random.Range(0, objects.Length);
            _b = UnityEngine.Random.Range(0, objects.Length);
            temp = arr[_a];
            arr[_a] = arr[_b];
            arr[_b] = temp;
        }

        return arr[UnityEngine.Random.Range(0, objects.Length)];
    }

    public Vector2 PositionOffset(Vector2 v)
    {
        float x = v.x + UnityEngine.Random.Range(rangeOffset.x, rangeOffset.y);
        float y = v.y + UnityEngine.Random.Range(rangeOffset.x, rangeOffset.y);
        return new Vector2(x, y);
    }
    #endregion
}
