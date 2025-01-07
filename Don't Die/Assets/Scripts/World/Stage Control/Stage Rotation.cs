using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using C_Math.ShapeRandomisation;
using C_Math;

public class StageRotation : MonoBehaviour
{
    public PlatformController[] platforms;
    public float rotationRadius;
    public GameObject rotationCenter;
    public float minRotation = 0;
    public float maxRotation = 360;
    public Vector3[] positions;
    public Circle2D circle;

    void Start()
    {

    }

    void Update()
    {
        RandomisePlatformsCircular();
    }

    public void RandomisePlatformsCircular()
    {
        circle.UpdateIncrement();
        positions = circle.GetPointsOnCircle(platforms.Length, transform.position);

        for (int i = 0; i < platforms.Length; i++)
        {
            platforms[i].MovePlatformTo(positions[i]);
        }
    }

    public bool regeneratePositions = false;

    private void OnDrawGizmos()
    {
        if(!Application.isPlaying) return;

        Gizmos.DrawIcon(rotationCenter.transform.position, "Rotation Center");
        //Gizmos.DrawWireSphere(rotationCenter.transform.position, rotationRadius);

        if (platforms.Length > 0)
        {
            if (regeneratePositions)
            {
                regeneratePositions = false;
                positions = circle.GetPointsOnCircle(platforms.Length, transform.position);
            }

            //Visualise the objects on the radius. 

            for (int i = 0; i < positions.Length; i++)
            {
                Gizmos.DrawSphere(positions[i], 0.5F);
            }
        }
    }
}
