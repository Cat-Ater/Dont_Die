using UnityEngine;

/// <summary>
/// Class responsible for updating UI components. 
/// </summary>
public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    
    [SerializeField]
    private UI_DialogueDisplay dialogueDisplay;
    
    public static UIManager Instance => instance;

    /// <summary>
    /// Returns the current game time as a formated string. 
    /// </summary>
    public string GetTimeSTR => GameManager.GameTimer.TimeToString();

    void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            DestroyImmediate(this);
        }
    }

    void Update()
    {

    }

    public void DisplayTest(ITextCaller caller, string[] lines, float scrollSpeed, float endlineWait) =>
        dialogueDisplay.SetInstanceUp(caller, lines, scrollSpeed, endlineWait);
}
