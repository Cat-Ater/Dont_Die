using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameManager _gameManager;

    public string GetTimeSTR => GameManager.GameTimer.TimeToString();

    void Start()
    {
        _gameManager = GameManager.Instance; 
    }

    void Update()
    {

    }
}
