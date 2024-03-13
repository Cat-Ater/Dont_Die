using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeDisplay : MonoBehaviour, ITextCaller
{
    private bool hasDisplayed = false;
    private bool isDisplaying = false;
    public string[] lines;
    public float dialogueSpeed = 0.5F;
    public float endlineWait = 0.5F;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isDisplaying && !hasDisplayed)
        {
            isDisplaying = true;
            UIManager.Instance.DisplayText(this, lines, dialogueSpeed, endlineWait);
        }
    }


    void ITextCaller.DisplayComplete()
    {
        hasDisplayed = true;
    }
}
