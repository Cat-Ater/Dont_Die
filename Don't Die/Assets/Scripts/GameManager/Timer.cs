using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float _current;
    Dictionary<string, float> _targetTimes; 

    public Timer()
    {
        _current = 0F;
        _targetTimes = new Dictionary<string, float>(); 
    }

    public void InsertTime(string label, float timeInSec)
    {
        _targetTimes.Add(label, timeInSec);
    }

    public bool GetTimeElapsed(string name)
    {
        if (_targetTimes.ContainsKey(name))
        {
            return (_current >= _targetTimes[name]);
        }
        Debug.Log("Key not located in timer");
        return false;
    }

    public void UpdateTimer(float dt)
    {
        _current += dt; 
    }
}
