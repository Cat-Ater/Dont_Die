using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class handling player hit collisions. 
/// </summary>
public class PlayerHitCollision : MonoBehaviour
{
    private static PlayerHitCollision hurtBox; 

    [SerializeField]
    private Collider2D hurtboxCollider;

    public void Start() => hurtBox = this;

    public static void SetColliderState(bool state) =>
        hurtBox.hurtboxCollider.enabled = state;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController.Alive = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 7)
        {
            PlayerController.Alive = false; 
        }
    }
}
