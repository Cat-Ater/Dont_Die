using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static bool Alive { get; set; } = true;

    public static bool PlayerEnabled { get; set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!Alive)
        {
            GameManager.Instance.SetDeathPosition(gameObject.transform.position);
            gameObject.SetActive(false);
        }
    }

    public static void CancelMovement()
    {

    }
}
