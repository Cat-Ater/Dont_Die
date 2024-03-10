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

    public Vector2 initalScale;
    public Vector2 scaleRange;
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
        _gameObject.transform.localScale += (Vector3)(_scaleStep * _currentTime);
    }
}

[System.Serializable]
public class BulletRotar
{
    private GameObject _gameObject;
    [SerializeField] private float currentTime;

    public float maxRotation;
    public float rotationSpeed;
    public float initalRotation;
    public float rotationTime; 
    public bool loop;

    private float RotationStep => maxRotation * rotationSpeed / rotationTime;

    public void SetGameObject(GameObject obj)
    {
        _gameObject = obj;
        _gameObject.transform.rotation = Quaternion.Euler(0, 0, initalRotation * Mathf.Rad2Deg);
    }

    public void Update()
    {
        if (currentTime > rotationTime && !loop)
            return;
        else if (currentTime >= rotationSpeed && loop)
            currentTime = 0;

        if (currentTime < rotationTime)
            currentTime += Time.deltaTime;

        _gameObject.transform.rotation = Quaternion.Euler(0, 0, (initalRotation + (RotationStep * currentTime)) * Mathf.Rad2Deg);
    }
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BulletComponent : MonoBehaviour
{
    [SerializeField]
    private BulletScaler _bulletScaler;
    [SerializeField]
    private BulletRotar _bulletRotar;
    [SerializeField]
    private Rigidbody2D _rigidbody2D;

    public float speed;
    public float lifeTime;
    public Vector2 direction;


    public void Start()
    {
        if (_rigidbody2D == null)
        {
            gameObject.TryGetComponent<Rigidbody2D>(out _rigidbody2D);
            if (_rigidbody2D == null)
                Debug.Log(gameObject.name + " Is missing rigidbody2D");
        }

        _bulletScaler.SetGameObject(this.gameObject);
        _bulletRotar.SetGameObject(gameObject);
    }

    public void Update()
    {
        _bulletScaler.Update();
        _bulletRotar.Update();
    }

    public static bool CompareVec2(Vector2 a, Vector2 b)
    {
        if (a.x > b.x && a.y > b.y)
            return true;
        return false;
    }
}
