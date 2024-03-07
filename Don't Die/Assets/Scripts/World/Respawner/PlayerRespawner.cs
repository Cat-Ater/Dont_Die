using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawner : MonoBehaviour
{
    public Vector2 respawnPoint;

    public void OnValidate()
    {
        respawnPoint = gameObject.transform.position;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
