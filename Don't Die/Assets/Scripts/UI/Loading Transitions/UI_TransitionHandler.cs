using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum TransitionType
{
    DEATH,
    TRANSITION,
    EXTRA,
    MAIN
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

    public void TransitionsState(IBroadcastTransitionState caller, TransitionType type, bool state, float speed)
    {
        this.caller = caller;
        foreach (TransitionColorSet item in transitionColors)
        {
            if (item.transitionType == type)
            {
                StartCoroutine(TransitionFader(panel, (state) ? item.max : item.min, speed));
                break;
            }
        }
    }

    private IEnumerator TransitionFader(Image img, Color b, float speed)
    {
        Color c = (b - img.color) / speed;
        float time = 0;

        while (time < speed)
        {
            time += Time.deltaTime;
            panel.color = c * time;
            yield return new WaitForSeconds(0.1f);
        }
        panel.color = b;
        caller.ChangeInState();
    }
}