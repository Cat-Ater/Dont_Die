using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GravestoneDisplay : MonoBehaviour, ITextCaller
{
    public float displayTimeOnExit = 1.5F;
    public bool isDisplaying = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player" || isDisplaying)
            return;

        if (!isDisplaying)
        {
            isDisplaying = true;

            //Display writable data here. 

            UIManager.Instance.DisplayText(this, new string[] { " " }, 0.02F, displayTimeOnExit);
        }
    }

    void ITextCaller.DisplayComplete()
    {
        isDisplaying = false; 
    }
}
