using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for handling the players movement. 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private SpriteRenderer _renderer;
    /// <summary>
    /// The players movement speed. 
    /// </summary>
    public float playerSpeed = 10F;
    /// <summary>
    /// Is the player currently dashing. 
    /// </summary>
    public bool dashing = false;
    /// <summary>
    /// Reference to the players rigidbody2D. 
    /// </summary>
    public Rigidbody2D body2D;

    public static Vector2 CurrentDirection { get; set; }

    private static float X { 
        get => CurrentDirection.x; 
        set => CurrentDirection = new Vector2(value, CurrentDirection.y); 
    }

    private static float Y
    {
        get => CurrentDirection.y;
        set => CurrentDirection = new Vector2(CurrentDirection.x, value);
    }

    public void Start()
    {
        //Make sure that the rigidbody2D is assigned. 
        if (body2D == null)
        {
            gameObject.TryGetComponent(out body2D);
            if (body2D == null)
                gameObject.SetActive(false);
        }

        _renderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        if(!PlayerController.PlayerEnabled || PlayerController.Immobilized)
            return;

        //Set the current direction to nothing. 
        CurrentDirection = new Vector2(0, 0);

        //Get the input updates. 
        UpdateInput();
    }

    /// <summary>
    /// Update the players current inputs. 
    /// </summary>
    private void UpdateInput()
    {
        //Check for right input
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            X = 1;

        //Check for left input
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            X = -1;

        //Check for up input
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            Y = 1;

        //Check for down input
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            Y = -1;
    }

    public void LateUpdate()
    {
        //Calculate and update movement vectors. 
        Vector2 normVec = CurrentDirection.normalized;
        Vector2 runningVec = normVec * playerSpeed;

        //Update the movement. 
        body2D.velocity = (runningVec);

        //Update the sprite direction. 

        if(CurrentDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(CurrentDirection.y, CurrentDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void CancelMovement()
    {

    }
}
