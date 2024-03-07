using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class defining system for handling player consumable item handling. 
/// </summary>
public class ConsumableHandler 
{
    /// <summary>
    /// The list of objects held by the system. 
    /// </summary>
    private static List<IConsumableDestruction> destructables = new List<IConsumableDestruction>();

    /// <summary>
    /// Add a IConsumableDestruction interface to the system. 
    /// </summary>
    public static IConsumableDestruction AddConsumableDestruction
    {
        set { destructables.Add(value); }
    }

    /// <summary>
    /// Remove an IConsumableDestruction interface from the system. 
    /// </summary>
    public static IConsumableDestruction RemoveConsumableDestruction
    {
        set { destructables.Remove(value); }
    }

    /// <summary>
    /// Call to Destroy all IConsumableDestructions interfaces held in the system. 
    /// </summary>
    public static void ConsumableDestruction()
    {
        IConsumableDestruction[] destructArr = destructables.ToArray();
        destructables = new List<IConsumableDestruction>();

        Debug.Log("Destruction called.");

        for (int i = destructArr.Length - 1; i > -1; i--)
        {
            Debug.Log(i.ToString());
            destructArr[i].OnDestruct();
        }
    }
}
