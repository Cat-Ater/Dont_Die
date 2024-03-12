using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static GameObject _player; 

    [SerializeField]
    static bool _playerAlive = true;

    public static GameObject PlayerObject => _player;

    public static bool Alive
    {
        get => _playerAlive;
        set
        {
            _playerAlive = value;
            if (_playerAlive == false)
            {
                GameManager.Instance.SetDeathPosition(_player.transform.position);
                if (!GameManager.PlayerRespawnable)
                {
                    _player.SetActive(false);
                    //TODO: load the entrance hall alternative. 
                    GameManager.LoadLevel("HoldingCell");
                }

                if (GameManager.PlayerRespawnable)
                {
                    _player.SetActive(false);
                    _player.transform.position = GameManager.Instance.respawner.respawnPoint;
                    Alive = true;
                    _player.SetActive(true);
                }
            }
        }
    }

    public static bool PlayerEnabled { get; set; } = true;

    public PlayerMovement playerMovement; 

    // Start is called before the first frame update
    void Start()
    {
        Alive = true;
        _player = gameObject;
        CameraController.SetNewTarget(_player);
    }

    public static void CancelMovement()
    {

    }
}
