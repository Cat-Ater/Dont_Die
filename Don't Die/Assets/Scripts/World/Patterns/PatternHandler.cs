using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PatternHandler : MonoBehaviour
{
    public Pattern[] patterns_Current; 
    public List<Pattern> patterns;
    public Pattern currentPattern; 
    public int currentIndex = 0; 


    public void Start()
    {
        patterns_Current = patterns.ToArray();
        currentPattern = GetPattern(currentIndex);
        GameManager.Instance.patternHandler = this; 
    }

    public void PatternCompleted()
    {
        if(currentIndex < patterns_Current.Length)
        {
            currentIndex++;
            currentPattern = GetPattern(currentIndex);
            currentPattern.OnStart(); 
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
