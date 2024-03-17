using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BulletScaler
{
    private GameObject _gameObject;
    private Vector2 _scaleStep;
    private float _currentTime;
    private float _scaleSeconds;


    [Header("The inital scale for the bullet.")]
    public Vector2 initalScale;
    [Header("The amount scale the bullet by.")]
    public Vector2 scaleRange;
    [Header("The rate the bullet at.")]
    public float scaleSpeed;

    public void SetGameObject(GameObject gObj)
    {
        _gameObject = gObj;
        _gameObject.transform.localScale = initalScale;
        _scaleSeconds = scaleSpeed * 60;
        _scaleStep = scaleRange / _scaleSeconds;
    }

    public void Update()
    {
        if (BulletComponent.CompareVec2(_gameObject.transform.localScale, (initalScale + scaleRange)))
        {
            return;
        }
        _currentTime += Time.deltaTime;
        if (_currentTime == 0)
            return;

        Vector2 value = (Vector2)initalScale + (Vector2)(_scaleStep * _currentTime);

        if (float.IsNormal(value.x) && float.IsNormal(value.y))
            _gameObject.transform.localScale = value;
    }
}

[System.Serializable]
public class BulletRotar
{
    private GameObject _gameObject;
    [SerializeField] private float currentTime;
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

    private float RotationStep => maxRotation / rotationTime;

    private float CurrentRotation => ((initalRotation + RotationStep) * Mathf.Deg2Rad);

    public void SetGameObject(GameObject obj)
    {
        _gameObject = obj;
        _gameObject.transform.rotation = Quaternion.Euler(0, 0, initalRotation * Mathf.Deg2Rad);
    }

    public void Update()
    {
        if ((currentTime > rotationTime || CurrentRotation * rotationSpeed * currentTime > maxRotation) && !loopRotation)
            return;
        else if (currentTime >= rotationSpeed && loopRotation)
            currentTime = 0;

        currentTime += Time.deltaTime;
        _gameObject.transform.rotation = Quaternion.Euler(0, 0, CurrentRotation * rotationSpeed * currentTime);
    }
}

public enum MovementType
{
    NONE,
    TARGET,
    DIRECTION,
    TO_POINT
}

[System.Serializable]
public class BulletMovement
{

    private Rigidbody2D _body2D;
    private float currentUpdateRate;
    private Vector2 lastUpdateVec;
    public MovementType movementType;

    public float movementSpeed;
    public float lifeTime;
    public GameObject movementTarget;
    public Vector2 movementDirection;
    public bool invertDirection = false;
    public Vector2 movementPoint;
    public float targetUpdateRate;

    public void SetRigidbody(Rigidbody2D body2D)
    {
        _body2D = body2D;
        currentUpdateRate = 0;
    }

    public void Update()
    {
        switch (movementType)
        {
            case MovementType.NONE:
                break;
            case MovementType.TARGET:
                Movement_Target();
                break;
            case MovementType.DIRECTION:
                Movement_Direction();
                break;
            case MovementType.TO_POINT:
                Movement_ToPoint();
                break;
        }
    }

    private void Movement_Target()
    {
        currentUpdateRate += Time.deltaTime;
        if (currentUpdateRate >= targetUpdateRate)
        {
            currentUpdateRate = 0;
            Vector2 i = _body2D.gameObject.transform.position;
            Vector2 b = movementTarget.gameObject.transform.position;
            lastUpdateVec = Vector2.Lerp(b - i, b, movementSpeed * Time.deltaTime);
        }

        _body2D.velocity = lastUpdateVec;
    }

    private void Movement_Direction()
    {
        _body2D.AddForce(((invertDirection) ? -movementDirection : movementDirection) * movementSpeed);
    }

    private void Movement_ToPoint()
    {
        Vector2 i = _body2D.gameObject.transform.position;
        Vector2 b = movementPoint;
        Vector2 lerpVec = Vector2.Lerp(b - i, b, movementSpeed * Time.deltaTime);
        _body2D.velocity = lerpVec;
    }
}

[System.Serializable]
public class BulletLifespan
{
    GameObject gameobject;
    public float lifeSpan;
    public bool hasLifespan = false;

    public void SetObject(GameObject gObj)
    {
        gameobject = gObj;
    }

    public void Update()
    {
        if (!hasLifespan)
            return;
        lifeSpan -= Time.deltaTime;
        if (lifeSpan <= 0)
            GameObject.Destroy(gameobject);
    }
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BulletComponent : DestructableObject
{
    [SerializeField]
    private BulletScaler _bulletScaler;
    [SerializeField]
    private BulletRotar _bulletRotar;
    public BulletMovement _bulletMovement;
    [SerializeField]
    private BulletLifespan _bulletLifeSpan;
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    public void Start()
    {
        if (_rigidbody2D == null)
        {
            gameObject.TryGetComponent<Rigidbody2D>(out _rigidbody2D);
            if (_rigidbody2D == null)
                Debug.Log(gameObject.name + " Is missing rigidbody2D");
        }

        _bulletLifeSpan.SetObject(this.gameObject);
        _bulletScaler.SetGameObject(this.gameObject);
        _bulletRotar.SetGameObject(gameObject);
        _bulletMovement.SetRigidbody(_rigidbody2D);
    }

    public void LateUpdate()
    {
        _bulletLifeSpan.Update();
        _bulletScaler.Update();
        _bulletRotar.Update();
        _bulletMovement.Update();
    }

    public static bool CompareVec2(Vector2 a, Vector2 b)
    {
        if (a.x > b.x && a.y > b.y)
            return true;
        return false;
    }

    public void SetData(BulletData data)
    {
        _bulletScaler.initalScale = data.initalScale;
        _bulletScaler.scaleRange = data.scaleRange; 
        _bulletScaler.scaleSpeed = data.scaleSpeed;

        _bulletRotar.maxRotation = data.maxRotation;
        _bulletRotar.rotationSpeed = data.rotationSpeed;
        _bulletRotar.initalRotation = data.initalRotation;
        _bulletRotar.rotationTime = data.rotationTime;
        _bulletRotar.loopRotation = data.loopRotation;

        _bulletMovement.movementType = data.movementType;
        _bulletMovement.movementSpeed = data.movementSpeed;
        _bulletMovement.movementTarget = data.movementTarget;
        _bulletMovement.targetUpdateRate = data.targetUpdateRate;
        _bulletMovement.movementDirection = data.movementDirection;
        _bulletMovement.movementPoint = data.movementPoint;
        _bulletMovement.invertDirection = data.invertDirection;
    }

    internal override void OnDestruction()
    {
        gameObject.SetActive(false);
    }
}
