using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Buzzsaw : MonoBehaviour
{
    private Rigidbody2D body2D;
    private Vector2 normDir;
    public float dist;
    public float traveledDist;
    public bool arrived = false;
    public Vector2 initalPosition;
    public Vector2 endPosition;
    public float delay;
    public float speed;
    public float arrivalThreshold = 0.15F;

    void Start()
    {
        body2D = gameObject.GetComponent<Rigidbody2D>();
        gameObject.transform.position = initalPosition;
        normDir = endPosition - initalPosition;
        normDir.Normalize();
        dist = (endPosition - initalPosition).magnitude;
    }

    void Update()
    {
        if (arrived)
            return;
        else
            UpdateMovement();
    }

    void UpdateMovement()
    {
        traveledDist = (endPosition - (Vector2)gameObject.transform.position).magnitude;
        if (traveledDist <= arrivalThreshold)
        {
            Debug.Log("Hit Zero Dist");
            arrived = true;
            gameObject.transform.position = endPosition;
            body2D.velocity = Vector2.zero;
            return;
        }
        else
        {
            //Update until object moves to the end point.
            body2D.velocity = normDir * speed * Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(initalPosition, 0.25F);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(endPosition, 0.25F);

        Gizmos.DrawLine(initalPosition, endPosition);
    }
}
