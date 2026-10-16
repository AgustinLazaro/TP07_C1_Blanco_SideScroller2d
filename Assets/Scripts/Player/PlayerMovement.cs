using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed = 9f;
    [SerializeField] private float jumpForce = 12f;

    [Header("Ground check")]    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius =0.2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    
    private float xInput;
    private bool RequestedJump;
    private bool isGrounded;
    private bool isCrouching;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
        float currentSpeed = moveSpeed;

        if (isCrouching)
        {
            currentSpeed = moveSpeed * 0.5f;
        }

        rb.linearVelocity = new Vector2(xInput * currentSpeed, rb.linearVelocity.y);

        if (RequestedJump)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            RequestedJump = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
       if (groundCheck != null)
       {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
       }
    }
}
