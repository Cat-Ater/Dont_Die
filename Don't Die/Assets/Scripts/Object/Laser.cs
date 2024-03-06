using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private Quaternion initalRotation;
    private Vector3 initalScale; 
    public GameObject sprite;
    public Vector3 pivotOffset;
    public Vector2 centerPos;
    public Vector2 endPos;
    public float scaleF = 0.1f;
    public float rotation = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        initalScale = gameObject.transform.localScale;
        initalRotation = gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = new Vector3(initalScale.x + scaleF, initalScale.y, initalScale.z);
        gameObject.transform.rotation = initalRotation * Quaternion.Euler(new Vector3(0, 0, rotation));
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(centerPos, endPos);
    }
}
