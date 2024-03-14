using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Arc
{
    public float initalTheta;
    public float endTheta; 
}

public class BulletSpawner : MonoBehaviour
{
    public BulletPattern pattern;
    public BulletData data; 
    public Vector2[] points;
    public GameObject[] gameObjs;
    public GameObject bulletPrefab;

#if DEBUG
    public float t = 0;
#endif

    private void OnValidate()
    {
        points = pattern.GetPattern();
    }

    void Start()
    {
        GenerateBullets();
    }

    void Update()
    {
        
    }

    public void GenerateBullets()
    {
        Vector3 position = gameObject.transform.position;
        Vector2 objectPos;
        gameObjs = new GameObject[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            objectPos = (Vector2)(position + (Vector3)points[i]);
            gameObjs[i] = GameObject.Instantiate(bulletPrefab, objectPos, Quaternion.identity);
            BulletComponent bComponent = gameObjs[i].GetComponent<BulletComponent>();
            bComponent.SetData(data);
            if (data.usePatternDirection)
                bComponent._bulletMovement.movementDirection = points[i].normalized;
        }

    }
#if DEBUG
    //public void OnDrawGizmos()
    //{
    //    Vector3 position = gameObject.transform.position;

    //    if (pattern != null)
    //    {
    //        points = pattern.GetPattern();
    //        if (points.Length > 0)
    //        {
    //            foreach (Vector2 v in points)
    //            {
    //                Gizmos.DrawSphere((Vector2)position + (v), 0.15f);
    //                Gizmos.DrawSphere((Vector2)position + (t * v), 0.15f);
    //                Gizmos.DrawLine((Vector2)position + v, (Vector2)position + (v.normalized * t));
    //            }
    //        }
    //    }
    //}
#endif
}

public abstract class BulletPattern : MonoBehaviour
{
    public abstract Vector2[] GetPattern();
}
