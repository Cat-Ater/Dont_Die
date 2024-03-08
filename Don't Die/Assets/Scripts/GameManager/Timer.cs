using System;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    float _current;
    Dictionary<string, float> _targetTimes;
    public float Time => _current;

    public bool Enabled { get; set; } = false;

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
        if (Enabled)
        {
            _current += dt;
        }
    }

    public string TimeToString()
    {
        string val = _current.ToString();

        if (_current >= 10)
        {
            if (val.Length > 5)
                return val.Substring(0, 5);
            else return val;
        }

        if (val.Length > 4 && _current <= 10)
            return val.Substring(0, 4);

        return val;
    }

    public void ResetTimer() => _current = 0;
}
