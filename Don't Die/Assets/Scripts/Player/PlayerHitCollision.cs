using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
/// <summary>
/// Class handling player hit collisions. 
/// </summary>
public class PlayerHitCollision : MonoBehaviour
{
    private static PlayerHitCollision hurtBox;

    [SerializeField]
    private Collider2D hurtboxCollider;

    public void Start() => hurtBox = this;

    public AudioClip buzzsawSlashAudioClip;
    public AudioClip laserZapAudioClip;
    public AudioSource source;

    public static void SetColliderState(bool state) =>
        hurtBox.hurtboxCollider.enabled = state;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Button" || collision.tag == "SystemTriggers" ||
            collision.gameObject.tag == "DeadBody")
            return;

        if (collision.gameObject.GetComponent<Buzzsaw>() != null)
        {
            GameObject obj = new GameObject();
            AudioSource source = obj.AddComponent<AudioSource>();
            source.PlayOneShot(buzzsawSlashAudioClip);
        }

        if (collision.gameObject.GetComponent<Laser>() != null)
        {
            GameObject obj = new GameObject();
            AudioSource source = obj.AddComponent<AudioSource>();
            source.PlayOneShot(laserZapAudioClip);
        }

        PlayerController.Alive = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeadBody")
            return;

        if(collision.gameObject.layer == 7)
        {
            PlayerController.Alive = false; 
        }
    }
}