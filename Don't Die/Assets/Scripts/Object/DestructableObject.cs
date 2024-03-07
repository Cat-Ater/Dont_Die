using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DestructableObject : MonoBehaviour, IConsumableDestruction
{

    internal void Awake()
    {
        GameManager.AddConsumableDestruction = this;
    }

    internal void OnDisable()
    {
        GameManager.RemoveConsumableDestruction = this;   
    }

    internal void OnDestroy()
    {
        GameManager.RemoveConsumableDestruction = this;
    }

    void IConsumableDestruction.OnDestruct() => OnDestruction();

    internal abstract void OnDestruction();
}
