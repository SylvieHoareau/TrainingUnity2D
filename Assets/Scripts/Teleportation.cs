using UnityEngine;

public class Teleportation : MonoBehaviour
{
    private Transform player;
    public Transform teleportTarget;
    public Vector3 teleportTargetPos; // muzzlePos

    public void Teleporter()
    {
        // player = GameObject.FindGameObjectWithTag("Player").transform;
        // teleportTarget = GameObject.FindGameObjectWithTag("TeleportTarget").transform;
        // player.position = teleportTarget.position;

        teleportTargetPos = teleportTarget.position;
        Debug.Log("Player teleported to: " + teleportTargetPos);
        player.position = teleportTargetPos;
        
    }
}
