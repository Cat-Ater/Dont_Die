using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GenericSaveRuntime;
using Unity.VisualScripting;

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
            LoadData(ref dHandler.data);

            instance = dHandler;
            DontDestroyOnLoad(dHandler);
            return dHandler;
        }
        else return instance;
    }

    public static void SetDeath() => instance.data.numberOfDeaths++;

    public static void SetExtraStageUnlocked() => instance.data.extraStageUnlocked = true; 

    public static void SaveData()
    {
        PlayerData data = instance.data;

        SerializationSchema schema = 
            new SerializationSchema() { 
                defaultFilename = "PD", 
                folderName = "PlayerData", 
                serializationType = SerializationFormat.XML 
            };

        GenericSaveHandler<PlayerData> dataSerializer = new GenericSaveHandler<PlayerData>(schema);
        dataSerializer.Save(data, "V1", OperationType.EXTERNAL);
    }

    public static void LoadData(ref PlayerData data)
    {
        SerializationSchema schema =
            new SerializationSchema()
            {
                defaultFilename = "PD",
                folderName = "PlayerData",
                serializationType = SerializationFormat.XML
            };

        GenericSaveHandler<PlayerData> dataSerializer = new GenericSaveHandler<PlayerData>(schema);
        PlayerData _data = dataSerializer.Load("V1", OperationType.EXTERNAL);
        
        if(_data == null)
        {
            data = new PlayerData();
            data.numberOfDeaths = 0;
            data.extraStageUnlocked = false;
        } else data = _data;
    }

    public static BossData FindReference(int bossID)
    {
        for (int i = 0; i < Data.bossDataRecords.Count; i++)
        {
            if (bossID == Data.bossDataRecords[i].bossID)
                return Data.bossDataRecords[i];
        }

        return null;
    }

    public static void SetNewBossTime(int bossID, float newTime)
    {
        for (int i = 0; i < Data.bossDataRecords.Count; i++)
        {
            if (bossID == Data.bossDataRecords[i].bossID)
            {
                Data.bossDataRecords[i].timeTakenToComplete = newTime; 
            }
        }
    }
}
