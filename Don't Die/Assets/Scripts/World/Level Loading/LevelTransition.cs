using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Class responsible for handling level transitions. 
/// </summary>
public class LevelTransition : MonoBehaviour
{
    public string levelName;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        GameManager.LoadLevel(levelName);
        gameObject.SetActive(false);
    }
}
