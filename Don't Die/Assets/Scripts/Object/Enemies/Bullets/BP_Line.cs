using System.Collections.Generic;
using UnityEngine;

public class BP_Line : BulletPattern
{
    public Vector2 endpoint;
    public int numberOfPoints = 10;

    public override Vector2[] GetPattern()
    {
        List<Vector2> vecs = new List<Vector2>();
        Vector2 distanceVec = (endpoint - (Vector2)gameObject.transform.position );
        float distance = distanceVec.magnitude;
        float step = distance / numberOfPoints;
        distanceVec.Normalize();

        for (int i = 1; i < numberOfPoints; i++)
        {
            vecs.Add(distanceVec * (step * i));
        }

        return vecs.ToArray();
    }
}