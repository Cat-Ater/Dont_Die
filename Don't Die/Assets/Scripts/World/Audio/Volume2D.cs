using System.Collections;
using UnityEngine;

/// <summary>
/// Class used to restrict volume to a certain range around an audiosource. 
/// </summary>
public class Volume2D : MonoBehaviour
{
    public Transform listenerTransform;
    public Vector2 offset; 
    public AudioSource audioSource;
    public float minDist = 1;
    public float maxDist = 400;
    public float minVol = 0;
    public float maxVol = 1; 

    void Update()
    {
        float dist = Vector3.Distance(transform.position + (Vector3)offset, listenerTransform.position);

        if (dist < minDist)
        {
            audioSource.volume = 1;
        }
        else if (dist > maxDist)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = Mathf.Clamp(1 - ((dist - minDist) / (maxDist - minDist)), minVol, maxVol);
        }
    }
}
