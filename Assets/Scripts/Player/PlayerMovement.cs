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
    private bool wasGrounded;

    [Header("Crouch Settings")]
    private bool isCrouching;
    private Vector2 standSize;
    private Vector2 standOffset;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource stepsSource;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip stepSound;
    [SerializeField] private AudioClip landingSound; 
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

        stepsSource.clip = stepSound;
    }

    void Update()
    {
        HandleInput();
        HandleJumpRequest();
        HandleSpriteFlip();
        HandleFootstepAudio();
        UpdateAnimations();
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        HandleMovementAndCrouching();
        ExecuteJump();
    }
    #endregion

    #region Input & Logic Methods
    private void HandleInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        isCrouching = Input.GetKey(KeyCode.LeftControl) && isGrounded;
    }

    private void HandleJumpRequest()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            RequestedJump = true;
            sfxSource.PlayOneShot(jumpSound);
        }
    }

    private void HandleSpriteFlip()
    {
        if (xInput != 0)
        {
            spriteRenderer.flipX = xInput < 0;
        }
    }

    private void HandleFootstepAudio()
    {
        bool isMoving = xInput != 0 && isGrounded;

        if (isMoving)
        {
            if (!stepsSource.isPlaying)
            {
                stepsSource.Play();
            }

            if (isCrouching)
            {
                stepsSource.pitch = 1.0f;
            }
            else
            {
                stepsSource.pitch = 1.3f;
            }
        }
        else
        {
            if (stepsSource.isPlaying)
            {
                stepsSource.Stop();
            }
        }
    }

    private void UpdateAnimations()
    {
        anim.SetFloat("Speed", Mathf.Abs(xInput));
        anim.SetBool("IsCrouching", isCrouching);
        anim.SetBool("IsGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
    #endregion

    #region Physics Methods
    private void CheckGrounded()
    {
        wasGrounded = isGrounded; 
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (isGrounded && !wasGrounded)
        { 
            sfxSource.PlayOneShot(landingSound);
        }
    }

    private void HandleMovementAndCrouching()
    {
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
    }

    private void ExecuteJump()
    {
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