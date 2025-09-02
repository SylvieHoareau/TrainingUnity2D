using UnityEngine;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour
{
    private Transform player;

    public Transform teleportStart;
    public Transform teleportTarget;
    public Vector3 teleportTargetPos; // muzzlePos

    [SerializeField] private bool isTeleported = false;

    [SerializeField] private Button button;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        button.onClick.AddListener(Teleporter);
    }
    public void Teleporter()
    {
        // teleportStart = GameObject.FindGameObjectWithTag("TeleportStart").transform;
        // teleportTarget = GameObject.FindGameObjectWithTag("TeleportTarget").transform;
        // player.position = teleportTarget.position;


        if (isTeleported)
        {
            teleportTargetPos = teleportTarget.position;
            Debug.Log("isTeleported false: " + isTeleported);
        }
        else
        {
            teleportTargetPos = teleportStart.position;
            Debug.Log("isTeleported true: " + isTeleported);
        }

        // Toggle
        isTeleported = !isTeleported;

        Debug.Log("Player teleported to: " + teleportTargetPos);
        player.position = teleportTargetPos;

    }
}
