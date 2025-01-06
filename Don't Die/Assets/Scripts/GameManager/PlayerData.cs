using System.Collections.Generic;

/// <summary>
/// Per round data. 
/// </summary>
public class RoundData
{
    public bool usedBomb;
    public bool usedBody;
}

/// <summary>
/// The players current data. 
/// </summary>
[System.Serializable]
public class PlayerData
{
    public string name;
    public int numberOfDeaths;
    public List<BossData> bossDataRecords;
    public bool extraStageUnlocked;
}

/// <summary>
/// Data tracked for boss completions. 
/// </summary>
public class BossData
{
    public string bossName;
    public int bossID;
    public bool bossComplete;
    public float timeTakenToComplete;
}
