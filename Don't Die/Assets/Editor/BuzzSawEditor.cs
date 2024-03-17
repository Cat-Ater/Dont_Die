using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Buzzsaw))]
public class BuzzSawEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        Buzzsaw buzzSaw = (Buzzsaw)target;

        Handles.color = Color.red;
        Vector2 newEnd = buzzSaw.endPosition;
        newEnd = Handles.PositionHandle(newEnd, Quaternion.identity);
        if (newEnd != buzzSaw.endPosition)
        {
            buzzSaw.endPosition = newEnd;
            EditorUtility.SetDirty(target);
        }

        //Draw Object functionality. 
        Handles.DrawLine(buzzSaw.gameObject.transform.position, buzzSaw.endPosition);
    }
}
