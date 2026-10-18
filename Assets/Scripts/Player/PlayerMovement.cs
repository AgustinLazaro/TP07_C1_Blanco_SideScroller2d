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

    public bool IsGrounded { get; private set; }
    private bool requestedJump;
    private bool wasGrounded;

    [Header("Crouch Settings")]
    public bool IsCrouching { get; private set; }
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
        IsCrouching = Input.GetKey(KeyCode.LeftControl) && IsGrounded;
    }
        
    private void HandleJumpRequest()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            requestedJump = true;
            sfxSource.PlayOneShot(jumpSound);
        }
    }

    private void HandleSpriteFlip()
    {
        if (xInput != 0)
        {
            float facingDirection;

            if (xInput > 0)
            {
                facingDirection = 1f;
            }
            else
            {
                facingDirection = -1f;
            }

            transform.localScale = new Vector3(facingDirection, 1f, 1f);
        }
    }

    private void HandleFootstepAudio()
    {
        bool isMoving = xInput != 0 && IsGrounded;

        if (isMoving)
        {
            if (!stepsSource.isPlaying)
            {
                stepsSource.Play();
            }

            if (IsCrouching)
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
        anim.SetBool("IsCrouching", IsCrouching);
        anim.SetBool("IsGrounded", IsGrounded);
        anim.SetFloat("yVelocity", rb.linearVelocity.y);
    }
    #endregion

    #region Physics Methods
    private void CheckGrounded()
    {
        wasGrounded = IsGrounded; 
        IsGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);

        if (IsGrounded && !wasGrounded)
        { 
            sfxSource.PlayOneShot(landingSound);
        }
    }

    private void HandleMovementAndCrouching()
    {
        float currentSpeed = moveSpeed;
        col.size = standSize;
        col.offset = standOffset;

        if (IsCrouching)
        {
            currentSpeed = moveSpeed * 0.5f;
            col.size = new Vector2(standSize.x, standSize.y / 2f);
            col.offset = new Vector2(standOffset.x, standOffset.y - (standSize.y / 4f));
        }

        rb.linearVelocity = new Vector2(xInput * currentSpeed, rb.linearVelocity.y);
    }

    private void ExecuteJump()
    {
        if (requestedJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            requestedJump = false;
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