using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for updating UI components. 
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    
    [SerializeField]
    private UI_DialogueDisplay dialogueDisplay;
    [SerializeField]
    private UI_TransitionHandler transitionHandler;
    
    public static UIManager Instance => instance;

    /// <summary>
    /// Returns the current game time as a formated string. 
    /// </summary>
    public string GetTimeSTR => GameManager.GameTimer.TimeToString();

    public void TransitionStateChange(IBroadcastTransitionState caller, TransitionType type, bool state, float speed)
    {
        transitionHandler.TransitionsState(caller, type, state, speed);
    }

    void Awake()
    {
        #region Singleton.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }
        #endregion
    }

    void Update()
    {

    }

    public void DisplayText(ITextCaller caller, string[] lines, float scrollSpeed, float endlineWait) =>
        dialogueDisplay.SetInstanceUp(caller, lines, scrollSpeed, endlineWait);
}
