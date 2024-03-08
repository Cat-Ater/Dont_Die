using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ConsumableDisplay : MonoBehaviour
{
    public Image[] consumableIcons; 

    void Start()
    {
        
    }

    void Update()
    {
        int val = PlayerConsumable.AvailableUses - 1; 

        if(consumableIcons.Length > 0)
        {
            for (int i = 0; i < consumableIcons.Length; i++)
            {
                if(val >= i)
                {
                    consumableIcons[i].enabled = true;
                }
                else
                {
                    consumableIcons[i].enabled = false;
                }
            }
        }
    }
}
