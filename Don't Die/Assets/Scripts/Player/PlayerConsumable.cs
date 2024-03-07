using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConsumable : MonoBehaviour
{
    public KeyCode inputKey = KeyCode.Tab;
    public bool used = false; 

    void Update()
    {
        if (Input.GetKeyDown(inputKey) && !used)
        {
            used = true;
            GameManager.Instance.ConsumableDestruction(); 
        }
    }
}
