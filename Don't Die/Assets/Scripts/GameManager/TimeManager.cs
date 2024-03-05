using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public Timer gameTimer = new Timer();

    public void Update()
    {
        gameTimer.UpdateTimer(Time.deltaTime);
    }
}
