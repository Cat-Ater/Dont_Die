using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string name;
    public int numberOfDeaths;
    public List<BossData> bossDataRecords;
    public bool extraStageUnlocked;
}

public class BossData
{
    public string bossName;
    public int bossID;
    public bool bossComplete;
    public float timeTakenToComplete;
}
