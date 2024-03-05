using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float _current;
    Dictionary<string, float> _targetTimes;
    public float Time => _current; 
    
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

    public string TimeToString()
    {
        string val = _current.ToString();
        string[] parts = val.Split('.');
        if (parts[1].Length > 2)
        {
            parts[1] = parts[1][0].ToString() + parts[1][1].ToString();
        }
        return parts[0] + "." + parts[1];
    }

    public void ResetTimer() => _current = 0; 
}
