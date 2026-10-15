using UnityEngine;

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
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
       
    }

    private void OnDrawGizmosSelected()
    {
       
    }
}
