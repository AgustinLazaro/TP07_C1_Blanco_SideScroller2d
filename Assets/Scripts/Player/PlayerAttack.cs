using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    private Animator anim;
    private PlayerMovement playerMovement;

    [Header("Data Configuration")]
    [SerializeField] private PlayerDataSO data;

    [Header("Attack Settings")]
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
        // Bug: este Update sigue leyendo el mouse con Time.timeScale = 0. Con la pausa abierta, hacer clic en un boton (por ejemplo Continue) instancia un proyectil.
        // Agrega un chequeo de pausa (Time.timeScale > 0 o un estado de juego) o EventSystem.current.IsPointerOverGameObject() antes de disparar.
        if (Input.GetMouseButtonDown(0) && playerMovement.IsGrounded)
        {
            ExecuteAttack();
        }
    }

    private void ExecuteAttack()
    {
        if (playerMovement.IsCrouching)
        {
            // Warning: strings magicos "isCrouchAttacking" e "isAttacking". Pasalos a constantes con Animator.StringToHash.
            // Ademas, el prefijo "is" es de bool y aca son Trigger: en PlayerController.controller conviene llamarlos Attack y CrouchAttack.
            anim.SetTrigger("isCrouchAttacking");
        }
        else
        {
            anim.SetTrigger("isAttacking");
        }

        // Suggestion: no hay cadencia de disparo. Se puede spamear el clic y spawnear un proyectil por clic sin limite. Agrega un fireRate en PlayerDataSO y un timer.
        GameObject projectile = Instantiate(data.projectilePrefab, firePoint.position, firePoint.rotation);

        if (transform.localScale.x < 0)
        {
            Vector3 projectileScale = projectile.transform.localScale;
            projectileScale.x = projectileScale.x * -1;
            projectile.transform.localScale = projectileScale;
        }
    }
    #endregion
}