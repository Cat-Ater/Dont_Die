using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used for handling player data. 
/// </summary>
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
            dHandler.data.mainStageComplete = false;
            dHandler.data.extrastageComplete = false;


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

    public static void SetMainStageComplete()
    {
        instance.data.mainStageComplete = true;
    }

    public static void SetExtraStageComplete()
    {
        instance.data.extrastageComplete = true;
    }

    public static void SetLastAttemptTime(float time)
    {
        if (instance.data.lastAttemptLength < time)
        {
            instance.data.longestTimeSurvived = time;
        }
        instance.data.lastAttemptLength = time;
    }

    public static void SetExtraStageUnlocked()
    {
        instance.data.mainStageComplete = true; 
    }
}
