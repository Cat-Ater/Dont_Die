using Core.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public struct RoundData
{
    public bool usedBomb;
    public bool usedBody;
}

/// <summary>
/// Class responsible for handling updating the game and acting as a system inbetween. 
/// </summary>
public class GameManager : MonoBehaviour, IBroadcastTransitionState
{
    /// <summary>
    /// The current static instance of the GameManager. 
    /// </summary>
    private static GameManager _instance;

    /// <summary>
    /// The current instance of the game timer. 
    /// </summary>
    private Timer _gameTimer;

    private ObjectManager objectManager;

    private PlayerRespawnHandler respawnHandler;

    private LevelLoading levelLoader;

    public BodyManager bodyManager;

    public PatternHandler patternHandler;

    /// <summary>
    /// Reference to the ObjectScheduler. 
    /// </summary>
    private ObjectScheduler _objectScheduler;

    /// <summary>
    /// The effects associated with the game. 
    /// </summary>
    public PlayerEffects effects;

    public RoundData roundData;

    #region Properties. 
    /// <summary>
    /// Returns the current GameManager. 
    /// </summary>
    public static GameManager Instance => _instance;

    /// <summary>
    /// Returns the current GameTimer instance. 
    /// </summary>
    public static Timer GameTimer => _instance._gameTimer;

    #region Respawning. 
    public static PlayerRespawner SetRespawn
    {
        set => Instance.respawnHandler.respawner = value;
    }
    #endregion

    #region Consumable Destruction. 
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
    #endregion

    #region Player Death.

    /// <summary>
    /// Add a position at which the player died. 
    /// </summary>
    /// <param name="position"> The position to add. </param>
    public Vector3 SetDeathPosition
    {
        set => PlayerDeath(value);
    }
    #endregion

    #region Player Spawning.
    /// <summary>
    /// Is the player able to respawn here. 
    /// </summary>
    public static bool PlayerRespawnable => Instance.respawnHandler.PlayerRespawnable;

    public Vector3 RespawnPosition => Instance.respawnHandler.Point;
    #endregion

    #region Player Completion.
    public static bool ExtraStageUnlocked => DataHandler.Data.extraStageUnlocked;
    #endregion

    #region Player Data. 

    #endregion
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
        if (UIManager.Instance == null)
            SceneManager.LoadSceneAsync("_UI", LoadSceneMode.Additive);

        //Set up player data. 
        DataHandler.CreateDataHandler();

        respawnHandler = new PlayerRespawnHandler();
        levelLoader = new LevelLoading();
    }

    void Update()
    {
        //Update system objects here. 
        _gameTimer.UpdateTimer(Time.deltaTime);
        _objectScheduler.UpdateObjects();
    }

    #region Player Death. 
    private void PlayerDeath(Vector2 position)
    {
        ShowEffects(position);
        PlayerDeathPostion(position);
        SetPlayerData();

        //Reset the game timer. 
        _gameTimer.Enabled = false;
        _gameTimer.ResetTimer();

        GameObject pObj = PlayerController.PlayerObject;

        if (PlayerRespawnable)
        {
            pObj.SetActive(false);
            pObj.transform.position = RespawnPosition;
            PlayerController.Alive = true;
            pObj.SetActive(true);
        }

        if (!PlayerRespawnable)
        {
            PlayerController.PlayerEnabled = false;
            pObj.SetActive(false);
            levelLoader.LoadLevel("HoldingCell", TransitionType.DEATH);
            PlayerController.PlayerEnabled = false;
        }

        //Load required level.
    }

    private void PlayerDeathPostion(Vector2 position)
    {
        bodyManager.AddPosition(DataHandler.Data.totalNumberOfDeaths, position, SceneManager.GetActiveScene().name);
    }

    private void SetPlayerData()
    {
        //Data updating. 
        DataHandler.UpdateData(GameManager.GameTimer);

    }

    private void ShowEffects(Vector2 position)
    {
        //Instantiate death visuals. 
        Instantiate(effects.RandomSplatter, effects.PositionOffset(position), Quaternion.identity);
        Instantiate(effects.RandomBody, position, Quaternion.Euler(effects.RotationOffset()));

    }
    #endregion

    #region Pattern Timers. 
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
    #endregion

    #region Object Management.
    public static GameObject GameObjectRequest(GameObject prefab, Vector2 position)
    {
        return Instance.objectManager.GetGameObjectType(prefab, position);
    }

    public static void CreateDeadBody(Vector2 position)
    {
        GameObject.Instantiate(GameManager.Instance.effects.ghostMouseHistoryPrefabs[0], position, Quaternion.identity);
        Instance.roundData.usedBody = true;
    }

    public static void UsedConsumable()
    {
        Instance.roundData.usedBomb = true;
        Instance.DestroyObjects();
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
    public void DestroyObjects() => ConsumableHandler.ConsumableDestruction();
    #endregion

    #region Level Loading.

    public static void LoadLevel(string name, TransitionType type)
    {
        if (type == TransitionType.MAIN)
        {
            Instance.roundData = new RoundData() { usedBody = false, usedBomb = false };
        }

        GameManager.Instance.levelLoader.LoadLevel(name, type);
    }
    #endregion

    public void MainCompletion()
    {
        Debug.Log("Main Completed");
        //Evaluate Results. 

        DataHandler.UpdateData(GameManager.GameTimer, (!roundData.usedBody && !roundData.usedBomb));
        GameTimer.Enabled = false; 

        levelLoader.LoadLevel("HoldingCell", TransitionType.TRANSITION);
    }

    public void ChangeInState()
    {
        throw new NotImplementedException();
    }
}
