using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConsumable : MonoBehaviour
{
    public KeyCode inputKey = KeyCode.Tab;
    private bool canUse = true;
    public int consumableCount = 1;
    public float cooldownPeriod = 0.5F; 

    public static int AvailableUses { get; set; } = 1;

    private bool CanActivate => Input.GetKeyDown(inputKey) && AvailableUses >= 1 && canUse == true; 

    void Update()
    {
        if (CanActivate)
        {
            canUse = false;
            AvailableUses--;
            GameManager.Instance.ConsumableDestruction();
            StartCoroutine(Cooldown());
        }
    }


    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldownPeriod);
        canUse = true; 
    }

    public static void ResetUses()
    {
        AvailableUses = 1; 
    }
}
