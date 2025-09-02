using UnityEngine;

public class Teleportation : MonoBehaviour
{
    private Transform player;
    public Transform teleportTarget;

    public void Teleporter()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        teleportTarget = GameObject.FindGameObjectWithTag("TeleportTarget").transform;
        player.position = teleportTarget.position;
    }
}
