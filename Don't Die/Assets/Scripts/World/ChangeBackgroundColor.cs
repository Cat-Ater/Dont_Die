using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeBackgroundColor : MonoBehaviour
{
    public Color mainNotCompleted;
    public Color extraNotCompleted;
    public Color extraCompleted;
    public Tilemap tilemap;

    private void Start()
    {
        if (DataHandler.Data.extraStageUnlocked == false)
        {
            tilemap.color = mainNotCompleted;
        }

        if(DataHandler.Data.extraStageUnlocked == true)
        {
            tilemap.color = extraNotCompleted; 
        }

        if (DataHandler.Data.extraStageComplete == true)
        {
            tilemap.color = extraCompleted;
        }
    }
}
