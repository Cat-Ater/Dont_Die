using UnityEngine;

/// <summary>
/// Class used for handling player respawning.
/// </summary>
[System.Serializable]
public class PlayerRespawnHandler
{
    /// <summary>
    /// Current assigned respawn point. 
    /// </summary>
    public PlayerRespawner respawner;

    /// <summary>
    /// Is the player able to respawn here. 
    /// </summary>
    public bool PlayerRespawnable => respawner != null;

    /// <summary>
    /// Returns the point at which the player should be spawned. 
    /// </summary>
    public Vector3 Point => respawner.respawnPoint;

    public void RespawnPlayer(ref GameObject player, Vector3 respawnPosition)
    {
        player.SetActive(false);
        player.transform.position = respawnPosition;
        PlayerController.PlayerEnabled = true;
    }

    public void RespawnPlayer(ref GameObject player)
    {
        player.SetActive(false);
        player.transform.position = Point;
        PlayerController.PlayerEnabled = true;
    }
}
