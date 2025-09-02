using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    [Header("Paramètres de mouvement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpingPower = 12f;

    public LayerMask groundLayer;
    public Transform groundCheck;

    private Rigidbody2D playerRb;
    private Animator playerAnimator;
    private PlayerInput playerInput;
    private float horizontal;
    private float vertical;

    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        if (playerRb == null) Debug.LogWarning("PlayerMove: pas de Rigidbody2D trouvé.");
        if (groundCheck == null) Debug.LogWarning("PlayerMove: groundCheck non assigné.");
        Debug.Log($"PlayerMove Start: PlayerInput={(playerInput!=null)}");

        if (playerInput != null && playerInput.actions != null)
        {
            Debug.Log("PlayerMove: actions available:");
            foreach (var a in playerInput.actions)
            {
                Debug.Log($" - {a.name}");
            }
        }
    }

    // Méthode appelée automatiquement par PlayerInput (Behavior = Send Messages)
    // Input Action "Move" doit exister et être de type Vector2
    public void OnMove(InputValue value)
    {
        if (value == null) return;
        Vector2 v = value.Get<Vector2>();
        horizontal = v.x;
        vertical = v.y;
        Debug.Log($"PlayerMove OnMove: value=({horizontal:F2},{vertical:F2})");
    }

    // Input Action "Jump" (Button)
    public void OnJump(InputValue value)
    {
        if (value == null || playerRb == null) return;
        if (value.Get<float>() > 0.5f && IsGrounded())
        {
            playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, jumpingPower);
        }
    }

    void FixedUpdate()
    {
        if (playerRb == null) return;
        playerRb.linearVelocity = new Vector2(horizontal * speed, playerRb.linearVelocity.y);
    }

    bool IsGrounded()
    {
        if (groundCheck == null) return false;
        return Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 0.1f), CapsuleDirection2D.Horizontal, 0f, groundLayer) != null;
    }

    void Update()
    {
        if (playerAnimator != null)
        {
            playerAnimator.SetFloat("speed", Mathf.Abs(horizontal));
            playerAnimator.SetBool("isGrounded", IsGrounded());
        }

        if (horizontal > 0.01f)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1f, 1f, 1f);
    }
}
