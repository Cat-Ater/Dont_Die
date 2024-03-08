using UnityEngine;

public class PlayerConsumablePickup : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "PLayer")
        {
            PlayerController.PlayerObject.GetComponent<PlayerConsumable>().consumableCount++;
            gameObject.SetActive(false);
        }
    }
}
