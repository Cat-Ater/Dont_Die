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

    public void ChangeInState()
    {
        if(stage == LevelTransitionStage.INIT_FADE)
        {
            stage = LevelTransitionStage.LOAD_LEVEL;
            SceneManager.LoadScene(levelToLoad);
            levelToLoad = "";
            stage = LevelTransitionStage.RELEASE; 
            UIManager.Instance.TransitionClear(1F);
        }
    }
}
