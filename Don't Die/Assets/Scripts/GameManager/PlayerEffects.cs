using UnityEngine;
/// <summary>
/// Class responsible for managing player effects. 
/// </summary>
[System.Serializable]
public class PlayerEffects
{
    /// <summary>
    /// Prefabs used for blood splatters. 
    /// </summary>
    public GameObject[] splatterPrefabs;

    /// <summary>
    /// Prefabs used for deady body drops. 
    /// </summary>
    public GameObject[] deadBodyPrefabs;

    /// <summary>
    /// Prefabs used for spawning ghost mice. 
    /// </summary>
    public GameObject[] ghostMouseHistoryPrefabs;

    /// <summary>
    /// Range used for offsetting bloodsplatters. 
    /// </summary>
    public Vector2 splatterRange = new Vector2(-1.5F, 1.5F);

    /// <summary>
    /// Returns random bloodsplatter prefab selection. 
    /// </summary>
    public GameObject RandomSplatter => SelectRandomPrefab(splatterPrefabs);

    /// <summary>
    /// Returns random body prefab selection. 
    /// </summary>
    public GameObject RandomBody => SelectRandomPrefab(splatterPrefabs);

    /// <summary>
    /// Returns offset vector using the postion provided.
    /// </summary>
    /// <param name="position"> The orignal position. </param>
    public Vector2 PositionOffset(Vector2 position)
    {
        return new Vector2(
            position.x + UnityEngine.Random.Range(splatterRange.x, splatterRange.y), 
            position.y + UnityEngine.Random.Range(splatterRange.x, splatterRange.y));
    }

    /// <summary>
    /// Returns randomly selected prefab. 
    /// Selection made using the Fisher-Yates algorithm. 
    /// </summary>
    /// <param name="objectArr"> The object array to randomly sample from. </param>
    private GameObject SelectRandomPrefab(GameObject[] objectArr)
    {
        int _a;
        int _b;
        GameObject temp;

        GameObject[] arr = objectArr;

        for (int i = 0; i < 20; i++)
        {
            _a = UnityEngine.Random.Range(0, objectArr.Length);
            _b = UnityEngine.Random.Range(0, objectArr.Length);
            temp = arr[_a];
            arr[_a] = arr[_b];
            arr[_b] = temp;
        }

        return arr[UnityEngine.Random.Range(0, objectArr.Length)];
    }
}
