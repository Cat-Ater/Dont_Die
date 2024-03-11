using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines the bullet pattern for a semiCircle.
/// </summary>
public class BP_Semicircle : BulletPattern
{
    public Arc arc = new Arc() { initalTheta = 0, endTheta = 180 };
    public float numberOfPoints;
    public float initalScale;

    public override Vector2[] GetPattern()
    {
        List<Vector2> points = new List<Vector2>();
        float cos;
        float sin;
        float step = arc.endTheta * Mathf.Deg2Rad / numberOfPoints;
        float currentTheta = arc.initalTheta * Mathf.Deg2Rad;

        for (int i = 0; i < numberOfPoints + 1; i++)
        {
            cos = Mathf.Cos(currentTheta);
            sin = Mathf.Sin(currentTheta);
            points.Add(new Vector2(cos, sin) * initalScale);
            currentTheta += step;
        }

        return points.ToArray();
    }
}
