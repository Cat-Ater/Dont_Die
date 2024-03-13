using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternHandler : MonoBehaviour
{
    public List<Pattern> patterns;
    public int currentIndex = -1; 


    public void Start()
    {
        GameManager.Instance.patternHandler = this;
        GameManager.Instance.ActivateTimer();
    }

    public void ActivatePatterns()
    {
        SetNext();
    }

    public void SetNext()
    {
        currentIndex++;
        patterns[currentIndex].OnStart(); 
    }

    public void PatternCompleted()
    {
        if(currentIndex < patterns.Count)
            SetNext();
    }

    public void Update()
    {
        patterns[currentIndex].Update(GameManager.GameTimer.Time);
        bool complete = patterns[currentIndex].PatternCompletion(GameManager.GameTimer.Time);
        if (complete)
        {
            Debug.Log("Pattern completed");
        }
    }

    public void OnButtonPress()
    {
        PatternCompleted();
    }

    public void OnConsumable()
    {
        PatternCompleted();
    }
}
