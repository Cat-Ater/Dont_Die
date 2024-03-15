using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines a circular bullet pattern. 
/// </summary>
public class BP_Circle : BulletPattern
{
    [Range(0, 20)]
    public float t = 0; 


    public Arc arc;
    public float numberOfPoints; 

    public override Vector2[] GetPattern()
    {
        List<Vector2> points = new List<Vector2>();
        float cos;
        float sin;
        float step = arc.endTheta * Mathf.Deg2Rad / numberOfPoints;
        float currentTheta = arc.initalTheta * Mathf.Deg2Rad;

        for (int i = 0; i < numberOfPoints; i++)
        {
            cos = Mathf.Cos(currentTheta);
            sin = Mathf.Sin(currentTheta);
            points.Add(new Vector2(cos, sin));
            currentTheta += step;
        }

        return points.ToArray();
    }
}
