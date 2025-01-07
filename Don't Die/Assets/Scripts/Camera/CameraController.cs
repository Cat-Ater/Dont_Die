using C_Math;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CameraMovementType
{
    FOLLOW_BOX,
    FOLLOW_SPHERE,
    FOCUS_OBJECT
}

public partial class CameraController : MonoBehaviour
{
    private static CameraController instance = null;
    public CameraMovementType type = CameraMovementType.FOLLOW_BOX;
    public CameraMovementType lastType;
    public GameObject target;
    public GameObject lastTarget;

    public static CameraMovementType Type => instance.type;

    public static Vector3 TargetPosition => instance.target.transform.position;

    public static void SetNewTarget(GameObject obj)
    {
        if (instance.target != null)
            instance.lastTarget = instance.target;
        instance.target = obj;
    }

    public static void SetCameraPos(Vector2 position)
    {
        instance.transform.position =
            new Vector3(position.x, position.y, instance.transform.position.z);
    }

    public static void RevertToLastTarget()
    {
        if (instance.lastTarget != null)
        {
            Debug.Log("CameraController: Resetting to last target.");
            instance.target = instance.lastTarget;
        }
        else
            Debug.Log("CameraController: No last target assigned.");
    }

    private void SetBehaviourType(CameraMovementType type)
    {
        lastType = type;
        this.type = type;
    }

    public void ReturnToPriorType() => type = lastType;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this.gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Alpha5))
            InterpolateZoom(4, 8.5f);
        if (Input.GetKeyUp(KeyCode.Alpha4))
            InterpolateZoom(-4, 8.5f);
    }

    private void LateUpdate()
    {

        switch (Type)
        {
            case CameraMovementType.FOLLOW_BOX:
                BoxFollowUpdate();
                break;
            case CameraMovementType.FOLLOW_SPHERE:
                SphereFollowUpdate();
                break;
            case CameraMovementType.FOCUS_OBJECT:
                FocusUpdate();
                break;
        }
        UpdateZoom();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            if (Type == CameraMovementType.FOLLOW_BOX)
                boxData.GetBox(transform.position).OnDraw();
            if (Type == CameraMovementType.FOLLOW_SPHERE)
                sphereData.GetSphere(transform.position).OnDraw();
        }

        if(target != null)
        {
            //Draw the current focused object. 
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(target.transform.position, 0.55F);
        }
    }
}

/////////////////////////////
/// Zoom handling.
/////////////////////////////
public partial class CameraController : MonoBehaviour
{
    private const float EPSILON = 0.00025f;
    private const float MIN_CAMERA = 1;
    private const float MAX_CAMERA = 100;

    [Range(1, 100)]
    [SerializeField] private float zoomLevel;
    [SerializeField] private bool zoomInterpolationActive = false;
    
    public void InterpolateZoom(float zoom, float period)
    {
        if (!zoomInterpolationActive)
        {
            zoomInterpolationActive = true;
            StartCoroutine(InterpolateZoomHandler(zoom, period));
        }
    }

    private IEnumerator InterpolateZoomHandler(float zoomDT, float period)
    {
        float inital = zoomLevel;
        float endValue = inital + zoomDT;
        float step = zoomDT / period;
        float current = inital;

        if(endValue < MIN_CAMERA) endValue = 1;

        if(Mathf.Sign(zoomDT) == -1)
        {
            while (current > endValue + EPSILON && current > MIN_CAMERA)
            {
                current = Mathf.Clamp(current + step, MIN_CAMERA, MAX_CAMERA);
                zoomLevel = current;
                yield return new WaitForSeconds(0.02F);
            }
        }

        if (Mathf.Sign(zoomDT) == 1)
        {
            while (current < endValue - EPSILON && current < MAX_CAMERA)
            {
                current = Mathf.Clamp(current + step, MIN_CAMERA, MAX_CAMERA);
                zoomLevel = current;
                yield return new WaitForSeconds(0.02F);
            }
        }

        zoomLevel = endValue;
        zoomInterpolationActive = false;
    }

    public void SetZoomLevel(float zoom)
    {
        zoomLevel = zoom; 
    }

    private void UpdateZoom()
    {
        Camera.main.orthographicSize = zoomLevel;
    }
}

/////////////////////////////
/// BOX FOLLOW.
/////////////////////////////
public partial class CameraController : MonoBehaviour
{
    public BoxDataComponent boxData;
    public float boxTrackingSpeed = 2f;

    public bool OutsideBounds
    {
        get
        {
            return !C_Math.Collision.AABB.AABBCollisionCheck(
                boxData.GetBox(transform.position),
                TargetPosition
                );
        }
    }

    private void BoxFollowUpdate()
    {
        if (Type != CameraMovementType.FOLLOW_BOX)
            return;

        if (OutsideBounds)
        {
            //TODO: Implement movement updating. 
            Vector3 cameraPos = transform.position;
            Vector3 targetPos = TargetPosition;
            targetPos.z = cameraPos.z;

            Vector3 follow = Vector3.Lerp(cameraPos, targetPos, boxTrackingSpeed * Time.deltaTime);

            SetCameraPos(follow);
        }
    }
}

/////////////////////////////
/// SPHERE FOLLOW.
/////////////////////////////
public partial class CameraController : MonoBehaviour
{
    public SphereDataComponent sphereData;
    public float sphereTrackingSpeed = 2f;

    public bool InsideBoundsSphere
    {
        get
        {
            return C_Math.Collision.Radial.RadialCheckInsideSphere(
                sphereData.GetSphere(transform.position),
                TargetPosition
                );
        }
    }

    private void SphereFollowUpdate()
    {
        if (Type != CameraMovementType.FOLLOW_SPHERE)
        {
            return;
        }

        if (!InsideBoundsSphere)
        {
            //TODO: Implement movement updating. 
            Vector3 cameraPos = transform.position;
            Vector3 targetPos = TargetPosition;
            targetPos.z = cameraPos.z;

            Vector3 follow = Vector3.Lerp(cameraPos, targetPos, sphereTrackingSpeed * Time.deltaTime);

            SetCameraPos(follow);
        }
    }
}

/////////////////////////////
/// TARGET POINT.
/////////////////////////////
public partial class CameraController : MonoBehaviour
{
    public enum FocusLockType
    {
        TIMED,
        LOCKED
    }

    public FocusLockType focusType;
    public Vector3 targetP;
    public float targetMoveSpeed = 1;
    public bool arrivedAtTarget = false;

    public void Focus(GameObject target, FocusLockType type, float focusTime)
    {
        Focus(target.transform.position, type, focusTime);
    }

    public void Focus(GameObject target, float moveSpeed, FocusLockType type, float immobilizeTime)
    {
        Focus(target.transform.position, moveSpeed, type, immobilizeTime);
    }

    public void Focus(Vector3 point, FocusLockType type, float immobilizeTime)
    {
        targetP = point;
        PlayerController.PlayerEnabled = false;
        PlayerController.Immobilize = immobilizeTime;

        if (type == FocusLockType.TIMED)
            StartCoroutine(TimerTillRelease());
    }

    public void Focus(Vector3 point, float moveSpeed, FocusLockType type, float immobilizeTime)
    {
        SetBehaviourType(CameraMovementType.FOCUS_OBJECT);
        this.focusType = type;
        targetP = point;
        targetMoveSpeed = moveSpeed;
        PlayerController.PlayerEnabled = false;
        PlayerController.Immobilize = immobilizeTime;
    }

    public IEnumerator TimerTillRelease()
    {
        yield return new WaitForSeconds(2.5F);
        ReturnToPriorType();
    }

    private void FocusUpdate()
    {
        if (CameraController.Type != CameraMovementType.FOCUS_OBJECT)
            return;

        Vector3 cameraPos = transform.position;
        Vector3 targetPos = targetP;
        targetPos.z = cameraPos.z;

        Vector3 lerpTowards =
            Vector3.Lerp(cameraPos, targetPos, targetMoveSpeed * Time.deltaTime);
        transform.position = lerpTowards;

        if (lerpTowards == Vector3.zero)
        {
            arrivedAtTarget = true;
            if (focusType == FocusLockType.TIMED)
                StartCoroutine(TimerTillRelease());
        }
    }
}
