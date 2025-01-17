using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : IBroadcastTransitionState
{
    internal enum LevelTransitionStage
    {
        NONE,
        INIT_FADE,
        LOAD_LEVEL,
        RELEASE
    }

    private LevelTransitionStage stage = LevelTransitionStage.NONE;
    private string levelToLoad;

    public void LoadLevel(string levelName, TransitionType type)
    {
        stage = LevelTransitionStage.INIT_FADE;
        levelToLoad = levelName;
        UIManager.Instance.TransitionStateChange(this, type, 0.18F);
    }

    public void LoadLevel(StageID stageID, TransitionType type)
    {
        stage = LevelTransitionStage.INIT_FADE;
        UIManager.Instance.TransitionStateChange(this, type, 0.18F);
    }

    public void ChangeInState()
    {
        if (stage == LevelTransitionStage.INIT_FADE)
        {
            stage = LevelTransitionStage.LOAD_LEVEL;
            SceneManager.LoadScene(levelToLoad);
            levelToLoad = "";
            stage = LevelTransitionStage.RELEASE;
            UIManager.Instance.TransitionClear(1F);
        }
    }

    #region New loading protocools. 
    public StageID currentLevel;
    public List<StageID> failedStages;

    private bool MainCC0 => EvaluateMainCC0();
    private bool ExtraACC0 => EvaluateFightCC0(StageID.EXTRAS_BOSS_A);
    private bool ExtraBCC0 => EvaluateFightCC0(StageID.EXTRAS_BOSS_B);

    public void LoadNext()
    {
        //Check if next loaded stage is ending or extras. 
        //If is extras check for failures. 
        //If extras failed load the first ending. 
        //If extras not failed proceed into extras. 

        switch (currentLevel)
        {
            case StageID.START:
                Debug.Log("LEVEL-LOAD-> Boss A.");
                LoadStateAdditively(StageID.BOSS_A);
                break;
            case StageID.BOSS_A:
                Debug.Log("LEVEL-LOAD-> Boss B.");
                LoadStateAdditively(StageID.BOSS_B);
                break;
            case StageID.BOSS_B:
                Debug.Log("LEVEL-LOAD-> Boss C.");
                LoadStateAdditively(StageID.BOSS_C);
                break;
            case StageID.BOSS_C:
                Debug.Log("LEVEL-LOAD-> Boss C.");
                //Need to check if Extras stage should be entered.
                if (MainCC0)
                {
                    //Then load next stage. 
                    Debug.Log("LEVEL-LOAD-> Boss C [MAIN 0CC, loading extras A].");
                    LoadStateAdditively(StageID.EXTRAS_BOSS_A);
                }
                else
                {
                    Debug.Log("LEVEL-LOAD-> Boss C [FAILED MAIN 0CC, loading ending A].");
                    //Divert to normal ending. 
                    currentLevel = StageID.ENDING_A;
                    SceneManager.LoadScene((int)currentLevel, LoadSceneMode.Single);
                }
                break;
            case StageID.EXTRAS_BOSS_A:
                Debug.Log("LEVEL-LOAD-> Extra_B.");
                if (ExtraACC0)
                {
                    //Load the second extras stage. 
                    Debug.Log("LEVEL-LOAD-> Extra_B [0CC PASSED, loading Extra B.");
                }
                break;
            case StageID.EXTRAS_BOSS_B:
                if (ExtraBCC0 && ExtraBCC0)
                {
                    //Load the Assimilation ending. 
                    Debug.Log("LEVEL-LOAD-> Assimilation ending.");
                    currentLevel = StageID.ENDING_C;
                    SceneManager.LoadScene((int)currentLevel, LoadSceneMode.Single);
                }
                else
                {
                    //Display the Secondary ending.
                    currentLevel = StageID.ENDING_B;
                    SceneManager.LoadScene((int)currentLevel, LoadSceneMode.Single);
                }

                break;
            case StageID.ENDING_A:
                LoadStateAdditively(StageID.CREDITS);
                break;
            case StageID.ENDING_B:
                LoadStateAdditively(StageID.CREDITS);
                break;
            case StageID.ENDING_C:
                LoadStateAdditively(StageID.CREDITS);
                break;
            case StageID.CREDITS:
                //Reset the game loop. 
                currentLevel = StageID.START;
                SceneManager.LoadScene((int)currentLevel, LoadSceneMode.Single);
                break;
            case StageID.GAME_HUB:
                break;
        }
    }

    public void LoadStateAdditively(StageID stage)
    {
        currentLevel = stage;

        //Load Async here.
        SceneManager.LoadSceneAsync((int)stage, LoadSceneMode.Additive);
    }

    public void FailedLevel()
    {
        if(!failedStages.Contains(currentLevel))
            failedStages.Add(currentLevel);
    }

    #region Stage Evaluation. 
    public bool EvaluateMainCC0()
    {
        bool completedMain = true;

        foreach (StageID stage in failedStages)
        {
            switch (stage)
            {
                case StageID.BOSS_A:
                    completedMain = false;
                    break;
                case StageID.BOSS_B:
                    completedMain = false;
                    break;
                case StageID.BOSS_C:
                    completedMain = false;
                    break;
                case StageID.EXTRAS_BOSS_A:
                    completedMain = false;
                    break;
                case StageID.EXTRAS_BOSS_B:
                    completedMain = false;
                    break;
            }
        }

        return completedMain;
    }

    public bool EvaluateFightCC0(StageID stage)
    {
        foreach (StageID stageID in failedStages)
        {
            if (stageID == stage)
            {
                return false;
            }
        }
        return true;
    }
    #endregion
    #endregion

}
