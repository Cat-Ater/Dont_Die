using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public Timer _gameTimer;

    public static GameManager Instance => _instance;
    public static Timer GameTimer => _instance._gameTimer;

    void Start()
    {

        //Create the singleton. 
        if(_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }

        //Set up timer
        _gameTimer = new Timer();
        _gameTimer.InsertTime("Game Completed", 60);

        //Load the UI into the scene if not present.
        SceneManager.LoadSceneAsync("_UI", LoadSceneMode.Additive);
    }

    void Update()
    {
        _gameTimer.UpdateTimer(Time.deltaTime); 
    }
}
