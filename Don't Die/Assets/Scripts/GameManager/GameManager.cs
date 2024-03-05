using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Timer gameTimer;  

    void Start()
    {
        gameTimer = new Timer();
        gameTimer.InsertTime("Game Completed", 60);
    }

    void Update()
    {
        gameTimer.UpdateTimer(Time.deltaTime); 
    }
}
