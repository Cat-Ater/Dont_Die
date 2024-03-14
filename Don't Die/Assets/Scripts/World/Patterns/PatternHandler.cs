using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternHandler : MonoBehaviour
{
    public List<Pattern> patterns;
    int currentIndex = -1;
    public bool complete = false;
    private bool activated = false;
    private bool CurrentPatternComplete =>
        patterns[currentIndex].PatternCompletion(GameManager.GameTimer.Time);
    private bool CanUpdate => currentIndex < patterns.Count;
    private Pattern GetCurrent => patterns[currentIndex];

    public void Start()
    {
        GameManager.Instance.patternHandler = this;
        GameManager.Instance.ActivateTimer();
    }

    public void ActivatePatterns()
    {
        activated = true;
        PatternCompleted();
    }

    public void PatternCompleted()
    {
        Debug.Log("Pattern completed");
        currentIndex++;
        Debug.Log("Current Index: " + currentIndex);
        if (currentIndex < patterns.Count)
        {
            Debug.Log("Updating pattern ID");
            GameManager.Instance.DestroyObjects();
            patterns[currentIndex].OnStart();
        }
        else
        {
            Debug.Log("Pattern Set Updated: Returning to Holding Cell");
            activated = false;
            GameManager.Instance.DestroyObjects();
            GameManager.Instance.MainCompletion();
        }
    }

    public void Update()
    {
        if (!activated || complete)
            return;

        GetCurrent.Update(GameManager.GameTimer.Time);

        //Check if pattern completed. 
        if (CurrentPatternComplete && CanUpdate)
        {
            PatternCompleted();
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
