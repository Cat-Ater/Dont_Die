using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum TransitionType
{
    DEATH,
    TRANSITION,
    EXTRA,
    MAIN, 
    CREDITS
}

[System.Serializable]
public struct TransitionColorSet
{
    public TransitionType transitionType;
    public Color min;
    public Color max;
}

public interface IBroadcastTransitionState
{
    void ChangeInState();
}

public class UI_TransitionHandler : MonoBehaviour
{

    public Image panel;
    public float fadeOutSpeed;
    public TransitionColorSet[] transitionColors;
    public IBroadcastTransitionState caller;

    public void Clear(float time)
    {
        StartCoroutine(ClearTimer(time));
    }

    private IEnumerator ClearTimer(float time)
    {
        yield return new WaitForSeconds(time);
        panel.color = panel.color + new Color(0, 0, 0, -panel.color.a);
    }

    public void TransitionsState(IBroadcastTransitionState caller, TransitionType type, float speed)
    {
        this.caller = caller;
        foreach (TransitionColorSet item in transitionColors)
        {
            if (item.transitionType == type)
            {
                StartCoroutine(TransitionFader(panel, item.max, speed));
                break;
            }
        }
    }

    private IEnumerator TransitionFader(Image img, Color b, float speed)
    {
        float time = 0;
        Color c = ((b - img.color) / speed);

        while (time < speed)
        {
            time += Time.deltaTime;
            panel.color = c * time;
            yield return new WaitForSeconds(0.1f);
        }
        panel.color = b;
        Debug.Log("Exited");
        caller.ChangeInState();
    }
}