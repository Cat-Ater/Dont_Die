using System.Collections;
using UnityEngine;

public class PatternRunner : MonoBehaviour
{
    public Pattern pattern;
    public Pattern currentRun;

    public bool repeat = false;

    public void Start()
    {
        CreateCopy();
        StartCoroutine(StartupDelay());
    }

    private void CreateCopy()
    {
        currentRun = new Pattern();
        currentRun.name = pattern.name;
        currentRun.order = pattern.order;
        currentRun.patternDialogues = pattern.patternDialogues;
        currentRun.patternEndTime = pattern.patternEndTime;
        currentRun.patternObjects = pattern.patternObjects;
    }

    public IEnumerator StartupDelay()
    {
        yield return new WaitForSeconds(5.5f);
        GameManager.GameTimer.Enabled = true;
    }

    public void Update()
    {

        pattern.Update(GameManager.GameTimer.Time);

        //Check if pattern completed. 
        if (currentRun.PatternCompletion(GameManager.GameTimer.Time) && repeat)
        {
            GameManager.Instance.DestroyObjects(); 
            CreateCopy();
            GameManager.GameTimer.ResetTimer();
            StartCoroutine(StartupDelay());
        }
        else if (currentRun.PatternCompletion(GameManager.GameTimer.Time) && !repeat)
        {
            Debug.Log("Pattern Completed");
        }
    }
}