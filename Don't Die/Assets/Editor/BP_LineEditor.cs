using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BP_Line))]
public class BP_LineEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        BP_Line line = (BP_Line)target;

        Vector2 newEnd;
        newEnd = Handles.PositionHandle(line.endpoint, Quaternion.identity);
        if ((newEnd = Handles.PositionHandle(line.endpoint, Quaternion.identity)) != line.endpoint)
        {
            line.endpoint = newEnd;
            EditorUtility.SetDirty(target);
        }

        //Draw object. 
        Handles.DrawLine(line.endpoint, line.gameObject.transform.position);
        Handles.color = Color.red;
        Vector2 pos = line.gameObject.transform.position;
        Vector2[] points = line.GetPattern();

        Handles.color = Color.red;
        foreach (Vector2 p in points)
        {
            Handles.DrawWireDisc(pos + p, new Vector3(0, 0, 1), 0.25F);
        }
        
    }
}
