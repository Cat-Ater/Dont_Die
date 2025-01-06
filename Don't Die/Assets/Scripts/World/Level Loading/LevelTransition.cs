using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for handling level transitions. 
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class LevelTransition : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    public StageID stageToLoad; 
    public float time;
    public float smoothing; 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        gameObject.GetComponent<BoxCollider2D>().enabled = false;

        audioSource.PlayOneShot(clip);
        GameManager.GameTimer.ResetTimer();
        GameManager.Instance.DisablePlayer();
        HandleTransition();
    }

    private void HandleTransition()
    {
        GameManager.LoadLevel(stageToLoad);
    }
}
