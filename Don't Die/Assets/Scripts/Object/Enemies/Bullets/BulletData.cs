using System.Collections;
using UnityEngine;

[System.Serializable]
public class BulletData 
{
    [Header("SCALE DATA:")]
    public float lifeSpan;
    public bool hasLifespan = false;

    [Header("SCALE DATA:")]
    [Header("The inital scale for the bullet.")]
    public Vector2 initalScale;
    [Header("The amount scale the bullet by.")]
    public Vector2 scaleRange;
    [Header("The rate the bullet at.")]
    public float scaleSpeed;

    [Header("ROTATION DATA:")]
    [Header("The maximum allowed rotation for the bullet.")]
    public float maxRotation;
    [Header("The rotation speed for the bullet.")]
    public float rotationSpeed;
    [Header("The inital rotation for the bullet.")]
    public float initalRotation;
    [Header("The maximum time the bullet should rotate.")]
    public float rotationTime;
    [Header("Should the rotation continue endlessly.")]
    public bool loopRotation;

    [Header("MOVEMENT DATA:")]
    [Header("The type of movement the bullet should use.")]
    public MovementType movementType;
    [Header("The speed of the bullet.")]
    public float movementSpeed;
    [Header("The Object the bullet should target.")]
    public GameObject movementTarget;
    [Header("How often the targets location should be updated.")]
    public float targetUpdateRate;
    [Header("The direction the bullet should move in.")]
    public Vector2 movementDirection;
    public bool usePatternDirection;
    public bool invertDirection; 
    [Header("The point the bullet should to.")]
    public Vector2 movementPoint;

}