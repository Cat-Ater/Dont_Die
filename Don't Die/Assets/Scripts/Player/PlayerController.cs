using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static GameObject _player;
    private static float immobilised = 0;
    [SerializeField]
    static bool _playerAlive = true;

    public static GameObject PlayerObject => _player;

    public static bool Alive
    {
        get => _playerAlive;
        set
        {
            _playerAlive = value;
            if (_playerAlive != false)
                return;

            //Else respond to death.
            PlayerMovement.CurrentDirection = Vector2.zero;
            GameManager.Instance.SetDeathPosition = _player.transform.position;
        }
    }

    public static float Immobilize
    {
        set
        {
            PlayerMovement.CurrentDirection = Vector2.zero;
            PlayerController.PlayerEnabled = false;
            immobilised = value;

        }
    }

    public static bool Immobilized => immobilised > 0;

    public static bool PlayerEnabled { get; set; } = true;

    public PlayerMovement playerMovement;

    void Start()
    {
        Alive = true;
        _player = gameObject;
        CameraController.SetNewTarget(_player);
    }

    private void Update()
    {
        if (Immobilized)
            immobilised -= Time.deltaTime;

        if (!Immobilized && !PlayerEnabled)
            PlayerEnabled = true; 

    }
}
