using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class DeadBody : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lasers")
            Debug.Log("Laser Collision on Dead body.");
        else
        {
            collision.gameObject.SetActive(false);
        }
    }
}
