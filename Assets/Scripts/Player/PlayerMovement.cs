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
    }

    private void FixedUpdate()
    {
       isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundLayer);
       rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
       
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
