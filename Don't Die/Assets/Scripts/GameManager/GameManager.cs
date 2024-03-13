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

    /// <summary>
    /// The current instance of the game timer. 
    /// </summary>
    private Timer _gameTimer;

    private DataHandler dHandler;
    private ConsumableHandler cHandler;
    public BodyManager bodyManager;
    public PatternHandler patternHandler; 
    private ObjectManager objectManager;
    private PlayerRespawnHandler respawnHandler; 

    /// <summary>
    /// Reference to the ObjectScheduler. 
    /// </summary>
    private ObjectScheduler _objectScheduler;

    /// <summary>
    /// The effects associated with the game. 
    /// </summary>
    public PlayerEffects effects;

    /// <summary>
    /// Returns the current GameManager. 
    /// </summary>
    public static GameManager Instance => _instance;

    /// <summary>
    /// Returns the current GameTimer instance. 
    /// </summary>
    public static Timer GameTimer => _instance._gameTimer;

    public static PlayerRespawner SetRespawn
    {
        set => Instance.respawnHandler.respawner = value; 
    }

    public void ActivateTimer()
    {
        StartCoroutine(StartupDelay());
    }

    public IEnumerator StartupDelay()
    {
        yield return new WaitForSeconds(5.5F);
        GameTimer.Enabled = true;
        patternHandler.ActivatePatterns();
    }

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

    /// <summary>
    /// Add a position at which the player died. 
    /// </summary>
    /// <param name="position"> The position to add. </param>
    public Vector3 SetDeathPosition
    {
        set
        {
            DataHandler.UpdateData(GameManager.GameTimer);
            bodyManager.AddPosition(DataHandler.Data.totalNumberOfDeaths, value, SceneManager.GetActiveScene().name);
            Instantiate(effects.RandomSplatter, effects.PositionOffset(value), Quaternion.identity);
            Instantiate(effects.RandomBody, value, Quaternion.Euler(effects.RotationOffset()));
        }
    }

    #region Player Spawning Properties.
    /// <summary>
    /// Is the player able to respawn here. 
    /// </summary>
    public static bool PlayerRespawnable => Instance.respawnHandler.PlayerRespawnable;

    public Vector3 RespawnPosition => Instance.respawnHandler.Point;
    #endregion

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
        if(UIManager.Instance == null)
            SceneManager.LoadSceneAsync("_UI", LoadSceneMode.Additive);

        //Set up player data. 
        dHandler = DataHandler.CreateDataHandler();

        respawnHandler = new PlayerRespawnHandler(); 
    }

    void Update()
    {
        //Update system objects here. 
        _gameTimer.UpdateTimer(Time.deltaTime);
        _objectScheduler.UpdateObjects();
    }

    #region Object Management.
    public static GameObject GameObjectRequest(GameObject prefab, Vector2 position)
    {
        return Instance.objectManager.GetGameObjectType(prefab, position);
    }
    #endregion

    #region Object Scheduling. 
    /// <summary>
    /// Call to add an object to scheduling. 
    /// </summary>
    /// <param name="obj"></param>
    public void ScheduleObject(ITimerScheduler obj) => _objectScheduler.AddScheduledObject(obj);
    #endregion

    #region Consumable Item.
    /// <summary>
    /// Call to Destroy all IConsumableDestructions interfaces held in the system. 
    /// </summary>
    public void ConsumableDestruction() => ConsumableHandler.ConsumableDestruction();
    #endregion

    #region Level Loading.
    public static void LoadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
    #endregion

    public void MainCompletion()
    {
        Debug.Log("Main Completed");
    }

    public void ResetGame()
    {
        _gameTimer.ResetTimer();
    }
}
