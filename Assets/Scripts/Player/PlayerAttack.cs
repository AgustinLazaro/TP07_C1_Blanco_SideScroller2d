using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    private Animator anim;
    private PlayerMovement playerMovement;

    [Header("Attack Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        HandleAttackInput();
    }
    #endregion

    #region Input & Logic Methods
    private void HandleAttackInput()
    {
        if (Input.GetMouseButtonDown(0) && playerMovement.IsGrounded)
        {
            ExecuteAttack();
        }
    }

    private void ExecuteAttack()
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
    #endregion
}