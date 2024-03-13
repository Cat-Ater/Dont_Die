using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for handling level transitions. 
/// </summary>
public class LevelTransition : MonoBehaviour, IBroadcastTransitionState
{
    public string levelName;
    public float time;
    public float smoothing; 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;
        UIManager.Instance.TransitionStateChange(this, TransitionType.MAIN, true, time);
    }

    void IBroadcastTransitionState.ChangeInState()
    {
        GameManager.LoadLevel(levelName);
        gameObject.SetActive(false);
    }
}
