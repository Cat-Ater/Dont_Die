using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private static GameObject _player;
    [SerializeField]
    static bool _playerAlive = true;
    private static float _immobilised = 0;

    public static GameObject PlayerObject => _player;

    public static bool Alive
    {
        get => _playerAlive;
        set
        {
            _playerAlive = value;
            if (_playerAlive == false)
                GameManager.Instance.SetDeathPosition = _player.transform.position;
        }
    }

    public static float Immobilize
    {
        set
        {
            PlayerMovement.CurrentDirection = Vector2.zero;
            PlayerController.PlayerEnabled = false;
            _immobilised = value;
            Rigidbody2D body2D = _player.gameObject.GetComponent<Rigidbody2D>();
            body2D.velocity = Vector2.zero;
        }
    }

    public static bool Immobilized => _immobilised > 0;

    public static bool PlayerEnabled { get; set; } = true;

    public PlayerMovement playerMovement; 

    void Start()
    {
        _player = this.gameObject; 
        Alive = true;
        CameraController.SetNewTarget(gameObject);
        if(PlayerConsumable.AvailableUses < 1)
        {
            PlayerConsumable.AvailableUses = 1; 
        }
    }

    private void Update()
    {
        if (Immobilized)
            _immobilised -= Time.deltaTime;

        if (!Immobilized && !PlayerEnabled)
            PlayerEnabled = true;
    }
}
