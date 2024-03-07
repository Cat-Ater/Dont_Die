using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour, IConsumableDestruction
{
    private Quaternion initalRotation;
    private Vector3 initalScale;
    [SerializeField] private float currentScale = 0F;
    private float scaleStep;
    private float currentTime;
    public GameObject child;
    public Vector3 pivotOffset;
    public Vector2 centerPos;
    public Vector2 endPos;
    public float maxScale = 0.1f;
    public float rotation = 0.1f;
    public float extendTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.AddConsumableDestruction = this; 
        initalScale = gameObject.transform.localScale;
        initalRotation = gameObject.transform.rotation;
        scaleStep = maxScale / extendTime;
        gameObject.transform.rotation = initalRotation * Quaternion.Euler(new Vector3(0, 0, rotation));
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime < extendTime)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= extendTime)
                currentTime = extendTime;
            currentScale = currentTime * scaleStep;
        }
        if (currentScale > maxScale)
            currentScale = maxScale;

        gameObject.transform.localScale = new Vector3(initalScale.x + currentScale, initalScale.y, initalScale.z);
    }

    private void OnDisable()
    {
        GameManager.RemoveConsumableDestruction = this; 
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(centerPos, endPos);
        Gizmos.color = Color.red;
    }

    void IConsumableDestruction.OnDestruct()
    {
        Debug.Log("Destruction called in lazer.");
        GameManager.RemoveConsumableDestruction = this; 
        Destroy(gameObject);
    }
}
