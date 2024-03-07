using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillSelf : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.SetDeathPosition(gameObject.transform.position);
        }       
    }
}
