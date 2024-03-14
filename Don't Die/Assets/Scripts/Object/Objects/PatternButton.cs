using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternButton : MonoBehaviour
{

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Button Pressed");
            GameManager.Instance.DestroyObjects();
            //TODO: Add calls to the world managment to allow pattern skipping. 
            Destroy(gameObject);
        }
    }
}
