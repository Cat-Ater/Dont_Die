using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for implementing the dash behaviour. 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerDash : MonoBehaviour
{
    /// <summary>
    /// Reference to the players rigidbody2D. 
    /// </summary>
    public Rigidbody2D body2D;
    /// <summary>
    /// Can the player dash. 
    /// </summary>
    private bool canDash = true;
    /// <summary>
    /// Is the player currently dashing. 
    /// </summary>
    private bool dashing = false;
    /// <summary>
    /// The players dash distance. 
    /// </summary>
    public float dashDistance = 10F;
    /// <summary>
    /// Time between the players dashes. 
    /// </summary>
    public float dashCooldown = 0.5F;

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
        dashing = (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && canDash;
    }

    private void LateUpdate()
    {
        Vector2 dashVec = PlayerMovement.CurrentDirection.normalized * (dashDistance * 1000);

        //If dashing apply the dash; Skip normal movement. 
        if (dashing)
        {
            canDash = false;
            PlayerHitCollision.SetColliderState(false);
            body2D.AddForce(dashVec);
            StartCoroutine(DashCooldown());
            return;
        }
    }

    /// <summary>
    /// Cooldown timer for the dash. 
    /// </summary>
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        PlayerHitCollision.SetColliderState(false);
        canDash = true;
    }
}
