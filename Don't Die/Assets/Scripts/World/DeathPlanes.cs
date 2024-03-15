using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlanes : MonoBehaviour
{
    private void Start()
    {
        gameObject.tag = "DeathPlane";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision occured on death plane.");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Collision Registered Player.");
            PlayerController.Alive = false;

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            PlayerController.Alive = false;
    }

}
