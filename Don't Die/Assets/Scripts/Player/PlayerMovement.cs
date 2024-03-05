using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 10F;
    public float dashDistance = 10F;
    public bool dashing = false;
    public bool canDash = true;
    public float dashCooldown = 0.5F; 
    public Vector2 currentDirection;
    public Rigidbody2D body2D;

    public void Start()
    {
        if (body2D == null)
        {
            gameObject.TryGetComponent<Rigidbody2D>(out body2D);
            if (body2D == null)
                gameObject.SetActive(false);
        }
    }


    public void Update()
    {
        currentDirection = new Vector2(0, 0);
        UpdateInput();
    }

    private void UpdateInput()
    {
        dashing = (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift));

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            //Apply right direction here
            currentDirection.x = 1;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            //Apply right direction here. 
            currentDirection.x = -1;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            //Apply right direction here. 
            currentDirection.y = 1;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            //Apply right direction here. 
            currentDirection.y = -1;
    }

    public void LateUpdate()
    {
        Vector2 normVec = currentDirection.normalized;
        Vector2 runningVec = normVec * playerSpeed;
        Vector2 dashVec = currentDirection * (dashDistance * 1000);

        if (dashing)
        {
            canDash = false; 
            body2D.AddForce(dashVec);
            StartCoroutine(DashCooldown());
            return;
        }
        body2D.velocity = (runningVec);
    }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
