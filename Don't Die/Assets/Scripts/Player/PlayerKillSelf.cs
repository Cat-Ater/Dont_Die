using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKillSelf : MonoBehaviour
{
    public float cooldownTime = 0.3f;
    public bool canUse = true;

    private static GameObject _self;

    private void Start()
    {
        _self = this.gameObject;
    }

    void Update()
    {
        if (canUse && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(KillSelfReset());
            KillSelf();
        }
    }

    public static void KillSelf()
    {
        PlayerController.Alive = false;     
    }

    private IEnumerator KillSelfReset()
    {
        canUse = false;
        yield return new WaitForSeconds(cooldownTime);
        canUse = true;
    }
}
