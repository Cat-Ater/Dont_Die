using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRotation
{
    GameObject _gameObject;
    Quaternion _initalRotation;
    float _currentRotationAngle;

    public LaserRotation(GameObject gameObject)
    {
        _gameObject = gameObject;
        _initalRotation = gameObject.transform.rotation;
    }

    public void ApplyRotation(float angle)
    {
        _currentRotationAngle = angle;
        _gameObject.transform.rotation =
            _initalRotation * Quaternion.Euler(new Vector3(0, 0, _currentRotationAngle));
    }

    public void ResetRotation()
    {
        _gameObject.transform.rotation = _initalRotation;
    }
}

[System.Serializable]
public class LaserHead
{
    private GameObject parent;
    private GameObject instance;

    public LaserHead(GameObject parent, GameObject prefab)
    {
        this.parent = parent;
        instance = GameObject.Instantiate(prefab);
        instance.transform.position = parent.transform.position;
        instance.transform.parent = parent.transform;
    }

    public void Update()
    {

    }
}

[System.Serializable]
public class LaserBody
{
    private GameObject parent;
    private GameObject instance;
    private Vector3 offsetVec = new Vector3(0.3F, 0F, 0F);
    private Vector3 initalScale = Vector3.one;
    public Vector3 scaleVec;

    public LaserBody(GameObject parent, GameObject prefab)
    {
        this.parent = parent;
        scaleVec.x = -1; 
        instance = GameObject.Instantiate(prefab);
        initalScale = instance.transform.localScale;
        instance.transform.parent = parent.transform;
        instance.transform.localPosition = parent.transform.position + offsetVec + (scaleVec / 2);
    }

    public void Update()
    {
        instance.transform.localPosition =  offsetVec + (scaleVec / 2);
        instance.transform.localScale = initalScale + scaleVec;
    }
}

public abstract class LaserBase : DestructableObject
{
    public LaserRotation lRotation;
    public LaserHead head;
    public LaserBody body;
    public GameObject startPrefab;
    public GameObject middlePrefab;

    public float currentScale;
    public float maxScale = 100F;
    public float scaleIncrement;
    public float scaleHalfIncrement;
    public float currentTime = 0F;
    public float extentsionTime = 1F;
    public float currentRotation = 30F; 

    private void Start()
    {
        Vector2 initalPosition = transform.position; 
        currentScale = 0;
        currentTime = 0;
        head = new LaserHead(this.gameObject, startPrefab);
        body = new LaserBody(this.gameObject, middlePrefab);
        lRotation = new LaserRotation(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        currentTime = Mathf.Clamp(currentTime, 0, extentsionTime);
        currentScale = (maxScale / extentsionTime) * currentTime;

        body.scaleVec.x = currentScale; 

        ////Update the local position.
        head.Update();
        body.Update();
        lRotation.ApplyRotation(currentRotation);
    }

    internal override void OnDestruction()
    {
        //Add Anim/Destruction Logic here.
        Destroy(gameObject);
    }
}
