using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadBody : MonoBehaviour
{
    public int hitCount = 2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
            hitCount--;

            if(hitCount <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
