using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlatformState
{
    ENABLED,
    ENABLING,
    DISABLED,
    DISABLING,
    FADING
}

public class PlatformController : MantleableObject
{
    public PlatformState state;
    public GameObject platform;

    public void DisablePlatform(float delayTime) => StartCoroutine(PlatformTimer(delayTime, Action_DisablePlatform));

    public void EnablePlatform(float delayTime) => StartCoroutine(PlatformTimer(delayTime, Action_EnablePlatform));

    public void RotatePlatformTo(float delayTime) => StartCoroutine(PlatformTimer(delayTime, Action_RotatePlatform));

    public void MovePlatformTo(Vector3 position)
    {
        float lastz = transform.position.z;
        gameObject.transform.position = position;
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, lastz);
    }

    public IEnumerator PlatformTimer(float timeTilComplete, Action completionAction)
    {
        yield return new WaitForSeconds(timeTilComplete);
        completionAction();
    }

    private void Action_DisablePlatform()
    {
        platform.SetActive(false);
    }

    private void Action_EnablePlatform()
    {
        platform.SetActive(true);
    }

    private void Action_RotatePlatform()
    {
        //TODO: Implement platform rotation. 
    }
}
