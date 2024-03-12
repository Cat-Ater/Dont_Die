using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternHandler : MonoBehaviour
{
    public Pattern[] patterns_Current; 
    public List<Pattern> patterns;
    public Pattern currentPattern; 
    public int currentIndex = -1; 


    public void Start()
    {
        patterns_Current = patterns.ToArray();
        GameManager.Instance.patternHandler = this;
        GameManager.Instance.ActivateTimer();
    }

    public void ActivatePatterns()
    {
        SetNext();
    }

    public void SetNext()
    {
        currentPattern = GetPattern(currentIndex++);
        currentPattern.OnStart(); 
    }

    public void PatternCompleted()
    {
        if(currentIndex < patterns_Current.Length)
            SetNext();
    }

    public void Update()
    {
        float time = GameManager.GameTimer.Time;

        currentPattern.OnTime(time);
    }

    public void OnButtonPress()
    {
        PatternCompleted();
    }

    public void OnConsumable()
    {
        PatternCompleted();
    }

    private Pattern GetPattern(int index)
    {
        for (int i = 0; i < patterns.Count; i++)
        {
            if (patterns_Current[i].order == index)
                return patterns_Current[i];
        }

        return null; 
    }
}
