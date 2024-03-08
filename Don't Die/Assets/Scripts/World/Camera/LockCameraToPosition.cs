using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for locking the camera to a given point. 
/// </summary>
public class LockCameraToPosition : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Lock the camera here. 
            CameraController.SetNewTarget(gameObject);
        }
    }
}
