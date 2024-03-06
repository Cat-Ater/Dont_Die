using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitCollision : MonoBehaviour
{
    private static PlayerHitCollision hurtBox; 

    [SerializeField]
    private Collider2D hurtboxCollider;

    public void Start()
    {
        hurtBox = this;
    }

    public static void SetColliderState(bool state)
    {
        hurtBox.hurtboxCollider.enabled = state;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Entered collsion.");
        GameManager.Instance.KillSelf(gameObject.transform.position);
    }
}
