using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public string levelName;
    public string currentLevelName; 

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        SceneManager.LoadScene(levelName);
        gameObject.SetActive(false);
    }
}
