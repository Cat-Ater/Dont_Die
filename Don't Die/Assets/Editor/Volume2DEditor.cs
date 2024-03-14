using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Volume2D))]
public class Volume2DEditor : Editor
{
    protected virtual void OnSceneGUI()
    {
        Volume2D vol = (Volume2D)target;

        Handles.DrawWireDisc(vol.gameObject.transform.position + (Vector3)vol.offset, new Vector3(0, 0, 1), vol.maxDist);
    }
}