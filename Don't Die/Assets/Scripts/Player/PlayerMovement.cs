using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for handling the players movement. 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Can the player dash. 
    /// </summary>
    private bool canDash = true;
    /// <summary>
    /// The players movement speed. 
    /// </summary>
    public float playerSpeed = 10F;
    /// <summary>
    /// The players dash distance. 
    /// </summary>
    public float dashDistance = 10F;
    /// <summary>
    /// Is the player currently dashing. 
    /// </summary>
    public bool dashing = false;
    /// <summary>
    /// Time between the players dashes. 
    /// </summary>
    public float dashCooldown = 0.5F;
    /// <summary>
    /// The current direction in which the player is going to dash. 
    /// </summary>
    public Vector2 currentDirection;
    /// <summary>
    /// Reference to the players rigidbody2D. 
    /// </summary>
    public Rigidbody2D body2D;

    public void Start()
    {
        //Make sure that the rigidbody2D is assigned. 
        if (body2D == null)
        {
            gameObject.TryGetComponent<Rigidbody2D>(out body2D);
            if (body2D == null)
                gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        //Set the current direction to nothing. 
        currentDirection = new Vector2(0, 0);

        //Get the input updates. 
        UpdateInput();
    }

    /// <summary>
    /// Update the players current inputs. 
    /// </summary>
    private void UpdateInput()
    {
        //Check for dash input. 
        dashing = (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift));

        //Check for right input
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            currentDirection.x = 1;

        //Check for left input
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            currentDirection.x = -1;

        //Check for up input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            currentDirection.y = 1;

        //Check for down input
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            currentDirection.y = -1;
    }

    public void LateUpdate()
    {
        //Calculate and update movement vectors. 
        Vector2 normVec = currentDirection.normalized;
        Vector2 runningVec = normVec * playerSpeed;
        Vector2 dashVec = currentDirection * (dashDistance * 1000);

        //If dashing apply the dash; Skip normal movement. 
        if (dashing)
        {
            canDash = false;
            body2D.AddForce(dashVec);
            StartCoroutine(DashCooldown());
            return;
        }
        else
        {
            //Update the normal movement. 
            body2D.velocity = (runningVec);
        }
    }

    /// <summary>
    /// Cooldown timer for the dash. 
    /// </summary>
    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
