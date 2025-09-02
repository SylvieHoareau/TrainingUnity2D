using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private PlayerControls controls;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;

    [Header("Paramètres de mouvement")]
    // Movement settings (editable in Inspector)
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpingPower = 12f;

    public LayerMask groundLayer;
    public Transform groundCheck;
    private float horizontal;

    private void Awake()
    {
        // Initialisation des contrôles
        controls = new PlayerControls();

        // Récupération du Rigidbody2D et de l'Animator
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();

   }

    private void OnEnable()
    {
        // Activation des contrôles
        controls.Enable();
    }

    private void OnDisable()
    {
        // Désactivation des contrôles
        controls.Disable();
    }

    // Called before performing any physics calculations
    private void FixedUpdate()
    {
        // Mouvements horizontaux
        playerRb.linearVelocity = new Vector2(horizontal * speed, playerRb.linearVelocity.y);
    }

    // Fonction associée à l'action "Move" dans le Input Action Asset (PlayerInput -> Send Messages)
    // PlayerInput (Behavior = Send Messages) appellera la méthode OnMove
    // Send Messages envoie un InputValue au lieu d'un CallbackContext
    public void OnMove(InputValue value)
    {
        if (value == null) return;
        Vector2 v = value.Get<Vector2>();
        horizontal = v.x;
    }

    // Permet de vérifier si le joueur est au sol
    bool IsGrounded()
    {
        // OverlapCapsule retourne un Collider2D (ou null)
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 0.1f), CapsuleDirection2D.Horizontal, 0f, groundLayer) != null;
    }

    // Fonction associée à l'action "Jump" (Send Messages)
    // PlayerInput appellera la méthode OnJump
    public void OnJump(InputValue value)
    {
        if (value == null) return;
        // Pour un bouton, Get<float>() retourne 1 lorsqu'il est pressé
        float pressed = value.Get<float>();
        if (pressed > 0.5f && IsGrounded())
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpingPower);
        }
    }

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        // Safety checks
        if (playerRb == null)
            Debug.LogWarning("PlayerMove: pas de Rigidbody2D trouvé sur le GameObject.");
        if (groundCheck == null)
            Debug.LogWarning("PlayerMove: pas de Transform 'groundCheck' assigné dans l'Inspector.");
    }

    void Update()
    {
        // Mise à jour de l'animation si un Animator est présent
        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("Speed", Mathf.Abs(horizontal));
            playerAnimator.SetBool("isGrounded", IsGrounded());
        }

        // Flip horizontal du sprite selon la direction
        if (horizontal > 0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}
