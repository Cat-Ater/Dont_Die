using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraToPosition : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Lock the camera here. 
            CameraController.SetNewTarget(gameObject);
        }
    }
}
