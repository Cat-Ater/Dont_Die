using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldState
{
    RESET,
    INACTIVE,
    ACTIVE, 
    PAUSED 
}

public class World : MonoBehaviour
{
    public WorldState worldState; 

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
