using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CMath.Collision
{
    public static class AABB
    {

        public static bool AABBCollisionCheck(Box box, Vector2 position)
        {
            Vector2 min = box.GetMin;
            Vector2 max = box.GetMax;

            if ((min.x < position.x && max.x > position.x) &&
                (min.y < position.y && max.y > position.y))
            {
                return true;
            }
            return false;
        }

        //TODO: TEST THIS FUNCTION. 
        public static bool AABBCollisionCheck(Box a, Box b)
        {
            Vector2 minA = a.GetMin;
            Vector2 maxA = a.GetMax;
            Vector2 minB = b.GetMin;
            Vector2 maxB = b.GetMax;

            if ((minA.x < minB.x && maxA.x > maxB.x) &&
                (minA.y < minB.y && maxA.y > minB.y))
            {
                return true;
            }
            return false;
        }
    }
}

[System.Serializable]
public class BoxDataComponent
{
    public float width;
    public float height;
    public GameObject obj;

    public Box GetBox => new Box()
    {
        center = obj.transform.position,
        height = height,
        width = width
    };
}

public enum CameraMovementType
{
    FOLLOW_BOX,
    DUNGEON_RAIL,
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

    private void FixedUpdate()
    {
        FocusUpdate();
        BoxFollowUpdate();
    }
}

/////////////////////////////
/// BOX FOLLOW.
/////////////////////////////
public partial class CameraController : MonoBehaviour
{
    public BoxDataComponent boxData;
    public float boxFollowSpeed = 2f;

    public bool OutsideBounds
    {
        get
        {
            return !CMath.Collision.AABB.AABBCollisionCheck(
                boxData.GetBox,
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

            Vector3 follow = Vector3.Lerp(cameraPos, targetPos, boxFollowSpeed * Time.deltaTime);

            SetCameraPos(follow);
        }
    }

    private void OnDrawGizmos()
    {
        boxData.GetBox.OnDraw();
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

    public void Focus(GameObject target, FocusLockType type)
    {
        Focus(target.transform.position, type);
    }

    public void Focus(GameObject target, float moveSpeed, FocusLockType type)
    {
        Focus(target.transform.position, moveSpeed, type);
    }

    public void Focus(Vector3 point, FocusLockType type)
    {
        targetP = point;
        PlayerController.PlayerEnabled = false;
        PlayerController.Immobilize = 9F; 

        if (type == FocusLockType.TIMED)
            StartCoroutine(TimerTillRelease());
    }

    public void Focus(Vector3 point, float moveSpeed, FocusLockType type)
    {
        SetBehaviourType(CameraMovementType.FOCUS_OBJECT);
        this.focusType = type;
        targetP = point;
        targetMoveSpeed = moveSpeed;
        PlayerController.PlayerEnabled = false;
        PlayerController.Immobilize = 9F;
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
