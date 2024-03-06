using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class responsible for pushing the time to the display. 
/// </summary>
public class UI_TimeDisplay : MonoBehaviour
{
    /// <summary>
    /// Reference to the UIManager instance. 
    /// </summary>
    public UIManager uiManager;
    /// <summary>
    /// The text component used to display the time. 
    /// </summary>
    public Text textDisplay;

    void Update()
    {
        //Do time readout update here. 
        textDisplay.text = uiManager.GetTimeSTR;
    }
}
