using System.Collections;
using UnityEngine;

public class MantleableObject : MonoBehaviour
{
    private enum ObjectMantleState
    {
        INACTIVE,
        ACTIVE
    }

    public Collider2D objectCollider;
    [SerializeField] private ObjectMantleState mantleState = ObjectMantleState.INACTIVE;

    private bool CheckObjectEnterConditions(Collider2D collider)
    {
        return (collider.transform.position.z > transform.position.z &&
            collider.gameObject.layer == 8);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mantleState == ObjectMantleState.INACTIVE &&
            CheckObjectEnterConditions(collision) && collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
            mantleState = ObjectMantleState.ACTIVE;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exited trigger");
        if (mantleState == ObjectMantleState.ACTIVE &&
            collision.gameObject.layer == 8 && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Exited trigger: Was player, de-parenting.");
            collision.transform.parent = null;
            mantleState = ObjectMantleState.INACTIVE;
            collision.transform.position = new Vector3(collision.transform.position.x, collision.transform.position.y, 0);
        }
    }

    private IEnumerator PlatformEnterTimer(float time)
    {
        yield return new WaitForSeconds(time);
    }

    private IEnumerator PlatformExitTimer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}