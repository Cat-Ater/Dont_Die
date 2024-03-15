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


[CustomEditor(typeof(BP_Circle)), CanEditMultipleObjects]
public class BP_CircleEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        BP_Circle line = (BP_Circle)target;

        Vector2[] points = line.GetPattern();

        foreach (Vector2 p in points)
        {
            Handles.DrawWireDisc(line.gameObject.transform.position + (Vector3)p, new Vector3(0, 0, 1), 0.05F);
            Handles.DrawSolidDisc(line.gameObject.transform.position + (Vector3)( line.t * p), new Vector3(0, 0, 1), 0.25F);
            Handles.DrawLine(line.gameObject.transform.position, line.transform.position + (Vector3)(20 * p));
        }

    }
}