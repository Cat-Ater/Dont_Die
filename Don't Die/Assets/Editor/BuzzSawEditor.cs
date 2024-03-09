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
        Handles.DrawLine(buzzSaw.gameObject.transform.position, buzzSaw.endPosition);
        Vector2 newEnd = buzzSaw.endPosition;
        newEnd = Handles.PositionHandle(buzzSaw.endPosition, Quaternion.identity);
        if (newEnd != buzzSaw.endPosition)
        {
            buzzSaw.endPosition = newEnd;
            EditorUtility.SetDirty(target);
        }
    }
}
