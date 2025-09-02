using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody2D playerRb;
    private Animator playerAnimator;

    [Header("Paramètres de mouvement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpingPower = 12f;

    public LayerMask groundLayer;
    public Transform groundCheck;
    private float horizontal;
    private float vertical;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        if (playerRb == null)
            Debug.LogWarning("PlayerMove: pas de Rigidbody2D trouvé sur le GameObject.");
        if (groundCheck == null)
            Debug.LogWarning("PlayerMove: pas de Transform 'groundCheck' assigné dans l'Inspector.");
    }

    private void OnEnable()
    {
        // Subscribe to actions from PlayerInput (more robust than Send Messages)
        if (playerInput != null && playerInput.actions != null)
        {
            // Try to find action by name (first try global name, then map/action)
            moveAction = playerInput.actions.FindAction("Move", false) ?? playerInput.actions.FindAction("Player/Move", false);
            jumpAction = playerInput.actions.FindAction("Jump", false) ?? playerInput.actions.FindAction("Player/Jump", false);

            if (moveAction != null)
            {
                moveAction.performed += OnMovePerformed;
                moveAction.canceled += OnMovePerformed; // to reset to zero when released
                moveAction.Enable();
                Debug.Log("PlayerMove: subscribed to Move action.");
            }

            if (jumpAction != null)
            {
                jumpAction.performed += OnJumpPerformed;
                jumpAction.Enable();
                Debug.Log("PlayerMove: subscribed to Jump action.");
            }
        }
        else
        {
            Debug.LogWarning("PlayerMove: PlayerInput or actions not found on this GameObject. Movement will not work.");
        }
    }

    private void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.performed -= OnMovePerformed;
            moveAction.canceled -= OnMovePerformed;
            moveAction.Disable();
            moveAction = null;
        }

        if (jumpAction != null)
        {
            jumpAction.performed -= OnJumpPerformed;
            jumpAction.Disable();
            jumpAction = null;
        }
    }

    // Called before performing any physics calculations
    private void FixedUpdate()
    {
        if (playerRb == null) return;
        // Mouvements horizontaux
        playerRb.linearVelocity = new Vector2(horizontal * speed, playerRb.linearVelocity.y);
    }

    // Callback when InputAction Move is performed/canceled (Input System events)
    private void OnMovePerformed(InputAction.CallbackContext ctx)
    {
        Vector2 v = ctx.ReadValue<Vector2>();
        horizontal = v.x;
        vertical = v.y;
        // Diagnostic log (only when there's a notable input to avoid spam)
        if (Mathf.Abs(horizontal) > 0.01f || Mathf.Abs(vertical) > 0.01f)
        {
            Debug.Log($"PlayerMove: OnMovePerformed value=({horizontal:F2},{vertical:F2})");
        }
    }

    // Callback when InputAction Jump is performed
    private void OnJumpPerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && IsGrounded() && playerRb != null)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpingPower);
        }
    }

    // Fonction associée à l'action "Move" si vous utilisez PlayerInput Send Messages
    public void OnMove(InputValue value)
    {
        if (value == null) return;
        Vector2 v = value.Get<Vector2>();
        horizontal = v.x;
        vertical = v.y;
    }

    // Permet de vérifier si le joueur est au sol
    bool IsGrounded()
    {
        if (groundCheck == null) return false;
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 0.1f), CapsuleDirection2D.Horizontal, 0f, groundLayer) != null;
    }

    // Fonction associée à l'action "Jump" si vous utilisez PlayerInput Send Messages
    public void OnJump(InputValue value)
    {
        if (value == null) return;
        float pressed = value.Get<float>();
        if (pressed > 0.5f && IsGrounded() && playerRb != null)
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpingPower);
        }
    }

    void Update()
    {
        // Mise à jour de l'animation si un Animator est présent
        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("speed", Mathf.Abs(horizontal));
            // Also update directional parameters if your Animator uses them
            playerAnimator.SetFloat("moveX", horizontal);
            playerAnimator.SetFloat("moveY", vertical);
            playerAnimator.SetBool("isGrounded", IsGrounded());
        }

        // Flip horizontal du sprite selon la direction
        if (horizontal > 0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}
