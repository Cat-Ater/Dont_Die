using UnityEngine;

public class DestroyObject : DestructableObject
{
    internal override void OnDestruction()
    {
        GameObject.Destroy(this.gameObject);
    }
}
