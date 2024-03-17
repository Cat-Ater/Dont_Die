using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillSelf : MonoBehaviour
{
    public float cooldownTime = 0.3f;
    public bool canUse = true;
    public int numberOfSpawns = 2; 
    public AudioClip spawnAudioClip;
    public AudioClip diedAudioClip;
    public AudioSource source; 
    
    private void OnEnable()
    {
        numberOfSpawns = 2; 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canUse)
        {
            if (numberOfSpawns > 0)
            {
                numberOfSpawns--;
                StartCoroutine(KillSelfReset());
                KillSelf(gameObject.transform.position);
                source.PlayOneShot(spawnAudioClip);
            }
            else
            {
                PlayerController.Alive = false;
                source.PlayOneShot(diedAudioClip);
            }
        }
        
    }

    public static void KillSelf(Vector2 position)
    {
        GameManager.CreateDeadBody(position);
    }

    private IEnumerator KillSelfReset()
    {
        canUse = false;
        yield return new WaitForSeconds(cooldownTime);
        canUse = true;
    }
}
