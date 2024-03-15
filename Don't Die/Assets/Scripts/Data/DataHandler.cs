using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    private static DataHandler instance;
    public PlayerData data;

    public static PlayerData Data => instance.data;

    /// <summary>
    /// Returns a new instance of the data handler. 
    /// </summary>
    public static DataHandler CreateDataHandler()
    {
        if (instance == null)
        {
            GameObject dataObj = new GameObject("DataHandler");
            DataHandler dHandler = dataObj.AddComponent<DataHandler>();

            //If handling data loading do so here. 
            dHandler.data = new PlayerData();
            dHandler.data.totalNumberOfDeaths = 0;
            dHandler.data.longestTimeSurvived = 0;
            dHandler.data.lastAttemptLength = 0;
            dHandler.data.extraStageUnlocked = false;
            dHandler.data.extraStageComplete = false;


            instance = dHandler;
            DontDestroyOnLoad(dHandler);
            return dHandler;
        }
        else
        {
            return instance;
        }
    }

    public static void SetDeath()
    {
        instance.data.totalNumberOfDeaths++;
    }

    public static void SetExtraStageComplete()
    {
        instance.data.extraStageComplete = true; 
    }

    public static void UpdateData(Timer gameTimer, bool died)
    {
        if (died)
            instance.data.totalNumberOfDeaths++;
        instance.data.lastAttemptLength = gameTimer.Time;
        instance.data.longestTimeSurvived =
            (gameTimer.Time > instance.data.longestTimeSurvived) ?
            gameTimer.Time : instance.data.longestTimeSurvived;
    }

    public static void UpdateData(Timer gameTimer, bool extraStageUnlocked, bool died)
    {
        if (died)
            instance.data.totalNumberOfDeaths++;
        instance.data.lastAttemptLength = gameTimer.Time;
        instance.data.longestTimeSurvived =
            (gameTimer.Time > instance.data.longestTimeSurvived) ?
            gameTimer.Time : instance.data.longestTimeSurvived;
        instance.data.extraStageUnlocked = extraStageUnlocked;
    }
}
