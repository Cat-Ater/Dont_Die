using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static GameObject player; 
    [SerializeField]
    static bool _playerAlive = true;

    public static bool Alive
    {
        get => _playerAlive;
        set
        {
            _playerAlive = value;
            if (_playerAlive == false)
            {
                GameManager.Instance.SetDeathPosition(player.transform.position);
                if (!GameManager.PlayerRespawnable)
                {
                    player.SetActive(false);
                    //TODO: load the entrance hall alternative. 
                }

                if (GameManager.PlayerRespawnable)
                {
                    player.SetActive(false);
                    player.transform.position = GameManager.Instance.respawner.respawnPoint;
                    Alive = true;
                    player.SetActive(true);
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
        player = gameObject; 
    }

    public static void CancelMovement()
    {

    }
}
