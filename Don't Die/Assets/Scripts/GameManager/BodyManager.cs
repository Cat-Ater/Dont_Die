using System.Collections.Generic;
using System.Linq; 
using UnityEngine;

[System.Serializable]
public struct BodyPos
{
    public Vector2 position;
    public string levelName; 

    public BodyPos(Vector2 position, string levelName)
    {
        this.position = position;
        this.levelName = levelName;
    }
}

[System.Serializable]
public class BodyManager
{
    public Dictionary<int, BodyPos> bodyPositions = new Dictionary<int, BodyPos>();

    public void AddPosition(int deathID, Vector2 point, string levelName)
    {
        bodyPositions.Add(deathID, new BodyPos(point, levelName));
    }

    public void OnBuildBodies(GameObject prefab, string name)
    {
        List<BodyPos> positions = bodyPositions.Values.ToList();

        for (int i = 0; i < positions.Count; i++)
        {
            if (positions[i].levelName == name)
            {
                GameObject obj = GameObject.Instantiate(prefab);
                obj.transform.position = positions[i].position;
            }
        }
    }
}
