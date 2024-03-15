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

    public bool hasDelay = false;
    public float bulletActivationDelay = 0.1f;
    public float distanceFromObj = 1;

    private void OnValidate()
    {
        points = pattern.GetPattern();
    }

    void Start()
    {
        GenerateBullets();
    }

    public void GenerateBullets()
    {
        Vector3 position = gameObject.transform.position;
        Vector2 objectPos;
        gameObjs = new GameObject[points.Length];
        for (int i = 0; i < points.Length; i++)
        {
            objectPos = (Vector2)(position + distanceFromObj * (Vector3)points[i]);
            gameObjs[i] = GameObject.Instantiate(bulletPrefab, objectPos, Quaternion.identity);
            BulletComponent bComponent = gameObjs[i].GetComponent<BulletComponent>();
            bComponent.SetData(data);
            if (data.usePatternDirection)
                bComponent._bulletMovement.movementDirection = points[i].normalized;
        }
        if (hasDelay)
        {
            StartCoroutine(BulletDelay(gameObjs, bulletActivationDelay));
        }
        else
        {
            for (int i = 0; i < gameObjs.Length; i++)
            {
                gameObjs[i].SetActive(true);
            }
        }
    }

    private IEnumerator BulletDelay(GameObject[] objects, float delay)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(true);
            yield return new WaitForSeconds(delay);
        }
    }
}

public abstract class BulletPattern : MonoBehaviour
{
    public abstract Vector2[] GetPattern();
}
