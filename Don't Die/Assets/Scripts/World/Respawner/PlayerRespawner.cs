using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    public Vector2 respawnPoint;

    void Start() => GameManager.SetRespawn = this; 
}
