using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Buzzsaw : DestructableObject
{
    private Rigidbody2D body2D;
    private Vector2 normDir;
    private float _currentDelay;
    private float traveledDist;
    private bool arrived = false;
    public Vector2 initalPosition;
    public Vector2 endPosition;
    public float delay;
    public float speed;
    public float arrivalThreshold = 0.15F;

    void Start()
    {
        GameManager.AddConsumableDestruction = this;

        //Get the component reference. 
        body2D = gameObject.GetComponent<Rigidbody2D>();
        //Place object at the inital transform. 
        gameObject.transform.position = initalPosition;
        normDir = endPosition - initalPosition;
        normDir.Normalize();
        _currentDelay = 0;
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
        if (_currentDelay < delay)
        {
            _currentDelay += Time.deltaTime;
            return;
        }
        traveledDist = (endPosition - (Vector2)gameObject.transform.position).magnitude;
        if (traveledDist <= arrivalThreshold)
        {
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

    internal override void OnDestruction()
    {
        //Add animation logic, destruction logic here.
        Destroy(gameObject);
    }
}
