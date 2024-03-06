using UnityEngine;

/// <summary>
/// Class responsible for updating UI components. 
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Returns the current game time as a formated string. 
    /// </summary>
    public string GetTimeSTR => GameManager.GameTimer.TimeToString();

    void Start()
    {
        
    }

    void Update()
    {

    }
}
