using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for detecting whether an object should be destroyed on collsion. 
/// </summary>
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DeadBody : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lasers" || collision.gameObject.tag == "Player")
            return;

        collision.gameObject.SetActive(false);
    }
}
