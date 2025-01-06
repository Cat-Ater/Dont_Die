using Core.Systems;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



/// <summary>
/// Class responsible for handling updating the game and acting as a system inbetween. 
/// </summary>
public partial class GameManager : MonoBehaviour, IBroadcastTransitionState
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

    /// <summary>
    /// Reference to the ObjectScheduler. 
    /// </summary>
    private ObjectScheduler _objectScheduler;

    /// <summary>
    /// The effects associated with the game. 
    /// </summary>
    public PlayerEffects effects;

    public RoundData roundData;

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
        Application.targetFrameRate = 60;

        //Create the singleton. 
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else DestroyImmediate(this);

        //Set up timer
        _gameTimer = new Timer();
        _gameTimer.InsertTime("Game Completed", 60);

        //Set up object scheduling. 
        _objectScheduler = new ObjectScheduler(_gameTimer);
        objectManager = new ObjectManager();

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

    public void ChangeInState()
    {
        throw new NotImplementedException();
    }
}


#region Data Management.
public partial class GameManager : MonoBehaviour, IBroadcastTransitionState
{
    public static bool ExtraStageUnlocked => DataHandler.Data.extraStageUnlocked;

    public void LoadData()
    {
        //Set up player data. 
        DataHandler.CreateDataHandler();
    }

    public void SaveData() => DataHandler.SaveData();

    public void AddBossCompletion(string bossName, int bossID, float timeTaken)
    {
        BossData data = DataHandler.FindReference(bossID);

        //If is null construct a new record to add. 
        if (data == null)
        {
            data = new BossData();
            data.bossID = bossID;
            data.bossName = bossName;
            data.timeTakenToComplete = timeTaken;
            //Insert the new record. 
            DataHandler.Data.bossDataRecords.Add(data);
        }
        else
        {
            //Else check if the new completion time is less than previous completion time. 
            if (data.timeTakenToComplete > timeTaken)
            {
                DataHandler.SetNewBossTime(bossID, timeTaken);
            }
        }

        DataHandler.SaveData();
    }
}
#endregion

#region Player Management. 
public partial class GameManager : MonoBehaviour, IBroadcastTransitionState
{

    /// <summary>
    /// Add a position at which the player died. 
    /// </summary>
    /// <param name="position"> The position to add. </param>
    public Vector3 SetDeathPosition
    {
        set => PlayerDeath(value);
    }

    public static PlayerRespawner SetRespawn
    {
        set => Instance.respawnHandler.respawner = value;
    }

    /// <summary>
    /// Is the player able to respawn here. 
    /// </summary>
    public static bool PlayerRespawnable => Instance.respawnHandler.PlayerRespawnable;

    public Vector3 RespawnPosition => Instance.respawnHandler.Point;

    public void PlayerDeath(Vector2 position)
    {
        ShowEffects(position);

        //Reset the game timer. 
        _gameTimer.Enabled = false;

        GameObject pObj = PlayerController.PlayerObject;

        DataHandler.SetDeath();
        PlayerController.PlayerEnabled = false;
        pObj.SetActive(false);

        if (PlayerRespawnable) respawnHandler.RespawnPlayer(ref pObj);
        else
        {
            levelLoader.LoadLevel("HoldingCell", TransitionType.DEATH);
            PlayerController.PlayerEnabled = true;
            _gameTimer.ResetTimer();
        }
    }

    private void ShowEffects(Vector2 position)
    {
        //Instantiate death visuals. 
        Instantiate(effects.RandomSplatter, effects.PositionOffset(position), Quaternion.identity);
        Instantiate(effects.RandomBody, position, Quaternion.Euler(effects.RotationOffset()));
    }

    public void DisablePlayer()
    {
        PlayerController.PlayerObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        PlayerController.PlayerObject.GetComponent<PlayerMovement>().enabled = false;
        PlayerController.PlayerEnabled = false;
    }

    public static void UsedConsumable()
    {
        Instance.roundData.usedBomb = true;
        Instance.DestroyObjects();
    }
}
#endregion

#region World State.
public partial class GameManager : MonoBehaviour, IBroadcastTransitionState
{
    #region Level Loading.
    public static void LoadLevel(StageID stageID)
    {
        TransitionType transitionType = TransitionType.TRANSITION;

        //Discern what load proceedure to use. 
        switch (stageID)
        {
            case StageID.START:
                Debug.Log("Game Start loading");
                transitionType = TransitionType.MAIN;
                break;
            case StageID.BOSS_A:
                Debug.Log("Boss A loading.");
                break;
            case StageID.BOSS_B:
                Debug.Log("Boss B loading.");
                break;
            case StageID.BOSS_C:
                Debug.Log("Boss C loading.");
                break;
            case StageID.BOSS_D:
                Debug.Log("Boss D loading.");
                break;
            case StageID.BOSS_E:
                Debug.Log("Boss E loading.");
                break;
            case StageID.BOSS_F:
                Debug.Log("Boss F loading.");
                break;
            case StageID.EXTRAS_BOSS_A:
                Debug.Log("Ex Boss A loading.");
                transitionType = TransitionType.EXTRA;
                break;
            case StageID.EXTRAS_BOSS_B:
                Debug.Log("Ex Boss B loading.");
                transitionType = TransitionType.EXTRA;
                break;
            case StageID.CREDITS:
                Debug.Log("Game credits loading.");
                transitionType = TransitionType.CREDITS;
                break;
            case StageID.GAME_HUB:
                Debug.Log("Game hub loading.");
                break;
        }

        Instance.levelLoader.LoadLevel(stageID, transitionType);
    }
    #endregion
}
#endregion

#region Object Management.
public partial class GameManager : MonoBehaviour, IBroadcastTransitionState
{
    public static GameObject GameObjectRequest(GameObject prefab, Vector2 position)
    {
        return Instance.objectManager.GetGameObjectType(prefab, position);
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
}
#endregion

//#region Empty.
//public partial class GameManager : MonoBehaviour, IBroadcastTransitionState
//{
//}
//#endregion