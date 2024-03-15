using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
/// <summary>
/// Class responsible for handling level transitions. 
/// </summary>
public class LevelTransition : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip gateClose; 

    public string levelName;
    public float time;
    public float smoothing; 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        audioSource.PlayOneShot(gateClose);
        GameManager.GameTimer.ResetTimer();
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
        PlayerController.PlayerEnabled = false;
        GameManager.LoadLevel(levelName, TransitionType.MAIN);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
