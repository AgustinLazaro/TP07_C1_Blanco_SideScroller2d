using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private Animator anim;
    private PlayerMovement playerMovement;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && playerMovement.IsGrounded)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (playerMovement.IsCrouching)
        {
            anim.SetTrigger("isCrouchAttacking");
        }
        else
        {
            anim.SetTrigger("isAttacking");
        }

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        if (transform.localScale.x < 0)
        {
            Vector3 projectileScale = projectile.transform.localScale;
            projectileScale.x = projectileScale.x * -1;
            projectile.transform.localScale = projectileScale;
        }
    }
}
