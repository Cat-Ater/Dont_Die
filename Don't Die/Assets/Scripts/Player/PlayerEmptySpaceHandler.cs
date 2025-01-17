#define DEBUG_GIZMOS
//#undef DEBUG_GIZMOS

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

internal class PlayerEmptySpaceHandler : MonoBehaviour
{
    public float castRadius = 2f;
    [Range(0.01f, 1)]
    public float gracePeriod = 0.5F;
    public bool isGrounded = false;
    public bool gracePeriodActive = false;
    public bool gracePeriodExpired = false;
    public PlayerDash playerDashComponent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isGrounded)
        {
            isGrounded = true;
            gracePeriodActive = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isGrounded)
        {
            isGrounded = true;
            gracePeriodActive = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isGrounded = false;
        gracePeriodActive = true;
        StartCoroutine(GracePeriodTimer());
    }

    private IEnumerator GracePeriodTimer()
    {
        if (playerDashComponent.Dashing)
        {
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(gracePeriod);

        if (!isGrounded && gracePeriodActive)
        {
            gracePeriodExpired = true;
            GameManager.Instance.PlayerDeath(transform.position);
        }
    }

#if DEBUG_GIZMOS
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, castRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + Vector2.right, castRadius);
        Gizmos.DrawLine((Vector2)transform.position + new Vector2(0, castRadius / 2), (Vector2)transform.position + new Vector2(0, castRadius / 2));
        Gizmos.DrawLine((Vector2)transform.position - new Vector2(0, castRadius / 2), (Vector2)transform.position - new Vector2(0, castRadius / 2));
    }
#endif
}
