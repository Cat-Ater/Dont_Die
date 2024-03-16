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
        if (DataHandler.Data.mainStageComplete == false)
        {
            tilemap.color = mainNotCompleted;
        }

        if (DataHandler.Data.mainStageComplete == true)
        {
            tilemap.color = extraNotCompleted;
        }

        if (DataHandler.Data.extrastageComplete == true)
        {
            tilemap.color = extraCompleted;
        }
    }
}