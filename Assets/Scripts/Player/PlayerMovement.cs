using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D col;
    private Animator anim;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 9f;
    private float xInput;

    [Header("Jump & Ground Settings")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;
    private bool RequestedJump;

    [Header("Crouch Settings")]
    private bool isCrouching;
    private Vector2 standSize;
    private Vector2 standOffset;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource sfxSource; 
    [SerializeField] private AudioSource stepsSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip stepSound;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        standSize = col.size;
        standOffset = col.offset;
    }

    void Update()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            RequestedJump = true;
        }
        if (xInput != 0) spriteRenderer.flipX = xInput < 0;

        isCrouching = Input.GetKey(KeyCode.LeftControl) && isGrounded;

        anim.SetFloat("Speed", Mathf.Abs(xInput));
        anim.SetBool("IsCrouching", isCrouching);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        float currentSpeed = moveSpeed;
        col.size = standSize;
        col.offset = standOffset;

        if (isCrouching)
        {
            currentSpeed = moveSpeed * 0.5f;
            col.size = new Vector2(standSize.x, standSize.y / 2f);
            col.offset = new Vector2(standOffset.x, standOffset.y - (standSize.y / 4f));
        }

        rb.linearVelocity = new Vector2(xInput * currentSpeed, rb.linearVelocity.y);

        if (RequestedJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            RequestedJump = false;
        }
    }
    #endregion

    #region Debug & Gizmos
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
        }
    }
    #endregion
}
