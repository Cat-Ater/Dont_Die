using UnityEngine;

[RequireComponent(typeof(AudioSource))]
/// <summary>
/// Class responsible for handling level transitions. 
/// </summary>
public class LevelTransitionExtraStage : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip clip;

    public void Start()
    {
        if (!GameManager.ExtraStageUnlocked)
        {
            Debug.Log("Disabling Extras Stage");
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        audioSource.PlayOneShot(clip);
        GameManager.GameTimer.ResetTimer();
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.gameObject.GetComponent<PlayerMovement>().enabled = false;
        GameManager.LoadLevel(StageID.EXTRAS_BOSS_A);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}