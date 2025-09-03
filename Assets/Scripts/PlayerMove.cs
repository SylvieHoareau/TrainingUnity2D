using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    // --- PARAMÈTRES (visibles dans l'Inspector) ---
    [Header("Paramètres de mouvement")]
    [SerializeField] private float speed = 5f;         // vitesse de déplacement horizontal
    [SerializeField] private float jumpingPower = 12f; // force appliquée au saut

    // Layer qui définit ce qui est considéré comme "sol"
    public LayerMask groundLayer;
    // point de vérification (objet vide placé sous le personnage) pour tester si on est au sol
    public Transform groundCheck;

    // --- RÉFÉRENCES (remplies au démarrage) ---
    private Rigidbody2D playerRb;    // composant physique du joueur (gère la position et la vitesse)
    private Animator playerAnimator; // contrôleur d'animations du joueur
    private PlayerInput playerInput; // composant du nouveau Input System (pour recevoir les entrées)

    // valeurs d'entrée provenant du Input System
    private float horizontal; // -1..1 pour gauche/droite
    private float vertical;   // généralement 0/1 pour haut/bas selon configuration

    // utilisé uniquement pour détecter si un autre script a déplacé le GameObject
    private Vector3 lastPosition;

    // --- Initialisation ---
    void Start()
    {
        // Récupère les composants présents sur le GameObject (si existants)
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        // Logs utiles si une référence manque (permet de comprendre plus facilement un problème)
        if (playerRb == null) Debug.LogWarning("PlayerMove: pas de Rigidbody2D trouvé.");
        if (groundCheck == null) Debug.LogWarning("PlayerMove: groundCheck non assigné.");
        Debug.Log($"PlayerMove Start: PlayerInput={(playerInput!=null)}");

        if (playerInput != null) Debug.Log($"PlayerInput component existe dans GameObject '{playerInput.gameObject.name}' et enverra des messages à ce GameObject.");

        // mémorise la position initiale pour détecter si un autre script a changé la position
        lastPosition = transform.position;

        // affiche la liste des actions configurées dans l'Input System (utile pour debug)
        if (playerInput != null && playerInput.actions != null)
        {
            Debug.Log("PlayerMove: actions available:");
            foreach (var a in playerInput.actions)
            {
                Debug.Log($" - {a.name}");
            }
        }
    }

    // -----------------
    // GESTION DES ENTRÉES
    // -----------------
    // Cette méthode est appelée automatiquement par le nouveau Input System
    // si le PlayerInput utilise le mode "Send Messages" et que l'action s'appelle "Move".
    // Elle récupère un Vector2 (par ex. joystick ou touches WASD) et stocke les valeurs.
    public void OnMove(InputValue value)
    {
        if (value == null) return;                    // protection contre valeur manquante
        Vector2 v = value.Get<Vector2>();            // récupère x/y du stick ou des touches
        horizontal = v.x;                             // -1..1 -> gauche/droite
        vertical = v.y;                               // utile si on veut un mouvement vertical
        Debug.Log($"PlayerMove OnMove: value=({horizontal:F2},{vertical:F2})");
    }

    // Méthode appelée par l'action "Jump" (bouton).
    // Si on appuie et qu'on est au sol, on applique une vitesse verticale (le saut).
    public void OnJump(InputValue value)
    {
        if (value == null || playerRb == null) return;
        // value.Get<float>() vaut 1 quand le bouton est pressé (selon configuration)
        if (value.Get<float>() > 0.5f && IsGrounded())
        {
            // on applique la composante verticale du saut, en conservant la vitesse horizontale actuelle
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpingPower);
        }
    }

    // -----------------
    // PHYSIQUE (FixedUpdate)
    // -----------------
    // FixedUpdate est appelé à intervalle fixe (bon pour la physique).
    void FixedUpdate()
    {
        if (playerRb == null) return; // sécurité

        // Log diagnostic : affiche l'entrée reçue, l'état du Rigidbody et la position
        Debug.Log($"PlayerMove FixedUpdate: horizontal={horizontal:F2} rb.bodyType={playerRb.bodyType} constraints={playerRb.constraints} vel=({playerRb.linearVelocity.x:F2},{playerRb.linearVelocity.y:F2}) pos=({transform.position.x:F2},{transform.position.y:F2})");

        // Applique le mouvement horizontal en réglant la vitesse du Rigidbody (meilleur que toucher transform directement)
        playerRb.linearVelocity = new Vector2(horizontal * speed, playerRb.linearVelocity.y);

        // Détecte si un autre script a changé la position du GameObject entre deux FixedUpdate
        if (transform.position != lastPosition)
        {
            Debug.Log($"PlayerMove: transform.position changed externally from ({lastPosition.x:F2},{lastPosition.y:F2}) to ({transform.position.x:F2},{transform.position.y:F2})");
        }
        lastPosition = transform.position;
    }

    // Vérifie si le joueur est au sol en testant une zone autour de 'groundCheck'.
    // Renvoie true si le personnage touche un collider appartenant à 'groundLayer'.
    bool IsGrounded()
    {
        if (groundCheck == null) return false;
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 0.1f), CapsuleDirection2D.Horizontal, 0f, groundLayer) != null;
    }

    // -----------------
    // VISUEL / ANIMATIONS
    // -----------------
    void Update()
    {
        // Met à jour les paramètres de l'Animator pour changer d'animation selon la vitesse et le fait d'être au sol
        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("speed", Mathf.Abs(horizontal));
            playerAnimator.SetBool("isGrounded", IsGrounded());
        }

        // Retourne la sprite du joueur selon la direction (flip horizontal)
        if (horizontal > 0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}
