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
            string a = "Best time: " + DataHandler.Data.longestTimeSurvived.ToString() + "\n";
            string b = "Last time: " + DataHandler.Data.lastAttemptLength.ToString() + "\n";
            string c = "Number of deaths: " + DataHandler.Data.totalNumberOfDeaths.ToString() + "\n";
            string[] output = new string[3] { a, b, c};


            UIManager.Instance.DisplayText(this, output, 0.02F, displayTimeOnExit);
        }
    }

    void ITextCaller.DisplayComplete()
    {
        isDisplaying = false; 
    }
}
