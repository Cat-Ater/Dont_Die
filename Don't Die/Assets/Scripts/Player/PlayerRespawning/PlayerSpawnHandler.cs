
using UnityEngine;

public static class PlayerSpawner
{
    public static void RespawnPlayer(ref GameObject player, Vector3 respawnPosition)
    {
        player.SetActive(false);
        player.transform.position = respawnPosition;
        PlayerController.PlayerEnabled = true;
    }
}