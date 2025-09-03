using UnityEngine;
// using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Teleportation : MonoBehaviour
{
    private Transform player;

    public Transform teleportStart;
    public Transform teleportTarget;

    [SerializeField] private bool isTeleported = false;

    [SerializeField] private Button button;

    // Pour contrôler avec la manette
    // private PlayerInput playerInput;
    // private InputAction teleportAction;

    // Au démarrage du jeu
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // playerInput = GetComponent<PlayerInput>();
        // teleportAction = playerInput.actions["Teleport"];

        // teleportAction.performed += ctx => Teleporter();

        if (button != null)
        {
            button.onClick.AddListener(Teleporter);
        }
    }
    public void Teleporter()
    {
        if (isTeleported) // Joueur au start -> téléporter au target
        {
            player.position = teleportTarget.position;
            Debug.Log("Téléporté vers Target");
        }
        else // Joueur au target -> téléporter au start
        {
            // teleportTargetPos = teleportStart.position;
            player.position = teleportStart.position;
            Debug.Log("Téléporté vers Start");
        }

        // Inverser l'état
        isTeleported = !isTeleported;
    }
}
