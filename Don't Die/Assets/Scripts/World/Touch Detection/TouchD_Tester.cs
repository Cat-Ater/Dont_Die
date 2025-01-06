using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchD_Tester : TouchResult
{
    internal override void Interaction()
    {
        Debug.Log("The object was touched and the result fired");
    }
}
