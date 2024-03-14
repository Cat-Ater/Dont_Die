using UnityEngine;

public class PlayingList : MonoBehaviour
{

    public AudioSource[] sources;

    void Start()
    {

        //Get every single audio sources in the scene.
        sources = GameObject.FindObjectsByType(typeof(AudioSource), FindObjectsSortMode.None) as AudioSource[];

    }

    void Update()
    {
        sources = GameObject.FindObjectsByType(typeof(AudioSource), FindObjectsSortMode.None) as AudioSource[];
    }
}