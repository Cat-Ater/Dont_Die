using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for timing an objects lifespan. 
/// </summary>
public class ObjectLifespan : MonoBehaviour
{
    public float time = 0F;
    public float timeLimit = 1F; 

    void LateUpdate()
    {
        time += Time.deltaTime; 
        if(time >= timeLimit)
        {
            gameObject.SetActive(false);
        }
    }
}
