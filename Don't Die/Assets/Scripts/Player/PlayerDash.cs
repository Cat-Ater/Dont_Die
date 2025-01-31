using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for implementing the dash behaviour. 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class PlayerDash : MonoBehaviour
{
    /// <summary>
    /// Reference to the players rigidbody2D. 
    /// </summary>
    public Rigidbody2D body2D;
    
    /// <summary>
    /// Can the player dash. 
    /// </summary>
    private bool _canDash = true;
    
    /// <summary>
    /// Is the player currently dashing. 
    /// </summary>
    private bool _dashing = false;
    
    /// <summary>
    /// The players dash distance. 
    /// </summary>
    public float dashDistance = 10F;

    /// <summary>
    /// Time between the players dashes. 
    /// </summary>
    public float dashCooldown = 0.5F;
    public float dashInvulnPeriod = 0.3F;
    public PlayerHitCollision hitCollision;
    public AudioClip dashAudioClip;
    public AudioSource source;

    public bool Dashing => _dashing;

    void Start()
    {
        //Make sure that the rigidbody2D is assigned. 
        if (body2D == null)
        {
            gameObject.TryGetComponent<Rigidbody2D>(out body2D);
            if (body2D == null)
                gameObject.SetActive(false);
        }
    }

    void Update()
    {
        //Check for dash input. 
        _dashing = (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && _canDash;
    }

    private void LateUpdate()
    {
        Vector2 dashVec;
        if (PlayerMovement.CurrentDirection != Vector2.zero)
            dashVec = PlayerMovement.CurrentDirection.normalized * (dashDistance * 1000);
        else
            dashVec = PlayerMovement.LastDirection.normalized * (dashDistance * 1000);

        //If dashing apply the dash; Skip normal movement. 
        if (_dashing)
        {
            _canDash = false;
            PlayerHitCollision.SetColliderState(false);
            hitCollision.enabled = false; 
            body2D.AddForce(dashVec);
            source.PlayOneShot(dashAudioClip);
            StartCoroutine(DashCooldown());
            return;
        }
    }

    /// <summary>
    /// Cooldown timer for the dash. 
    /// </summary>
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashInvulnPeriod);
        PlayerHitCollision.SetColliderState(true);
        yield return new WaitForSeconds(dashCooldown - dashInvulnPeriod);
        _canDash = true;
    }

    private void OnDisable()
    {
        StopAllCoroutines(); 
        _canDash = true;
        _dashing = false; 
    }
}
