using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetector : MonoBehaviour
{
    public List<TouchResult> touchResultHandlers;
    private bool activated = false;

    public bool Activated => activated;

    private void Start()
    {
        Anim_OnEnable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 8 && !activated)
        {
            activated = true;

            Anim_OnInteraction();

            for (int i = 0; i < touchResultHandlers.Count; i++)
            {
                touchResultHandlers[i].Interaction();
            }
        }
    }

    public void Anim_OnEnable()
    {
        //Place touchpoint animation logic here. 
    }

    public void Anim_OnInteraction()
    {
        //Place touchpoint animation logic here. 
    }
}