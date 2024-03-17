using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Buzzsaw : DestructableObject
{
    private Rigidbody2D body2D;
    public float delay;
    public float speed;
    public AudioClip onHitClip;
    public Vector2 initalPosition;
    public Vector2 endPosition;
    public float startTime;
    public float delayTime;
    public float distance;

    void Start()
    {
        GameManager.AddConsumableDestruction = this;
        body2D = gameObject.GetComponent<Rigidbody2D>();

        initalPosition = gameObject.transform.position;
        startTime = Time.time;
        delayTime = delay;
        distance = Vector2.Distance(initalPosition, endPosition);
        gameObject.transform.position = initalPosition;
    }


    void LateUpdate()
    {
        if (delayTime > 0)
            delayTime -= Time.deltaTime;
        float distCovered = (Time.time - startTime) * speed;
        float frac = distCovered / distance;

        Vector3 end = Vector2.Lerp(initalPosition, endPosition, frac);
        body2D.MovePosition(end);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            Gizmos.DrawLine(gameObject.transform.position, (Vector2)endPosition);
        else
            Gizmos.DrawLine(initalPosition, endPosition);

    }

    internal override void OnDestruction()
    {
        //Add animation logic, destruction logic here.
        gameObject.SetActive(false);
    }
}
