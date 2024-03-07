using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRespawner : MonoBehaviour
{
    public GameObject prefab; 
    public Vector2 spawnPos;
    public float initalDelay = 1F;
    public float normalDelay = 5F;

    private void OnValidate()
    {
        spawnPos = gameObject.transform.position; 
    }

    void Start()
    {
        StartCoroutine(SpawnTimer(initalDelay));
    }

    private IEnumerator SpawnTimer(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnItem();
    }

    private void SpawnItem()
    {
        GameObject.Instantiate(prefab, spawnPos, Quaternion.identity);
        StartCoroutine(SpawnTimer(normalDelay));
    }
}
