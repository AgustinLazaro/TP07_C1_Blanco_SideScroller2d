using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerHealth : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    private HUDController hud;
    private Animator anim;
    private PlayerMovement playerMovement;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    [Header("Audio Settings")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip gameOverSound;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
        hud = FindFirstObjectByType<HUDController>();
        hud.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        HandleDebugInput();
    }
    #endregion

    #region Input & Logic Methods
    private void HandleDebugInput()
    {
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20f);
        }
    }

    private void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        hud.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            audioSource.PlayOneShot(gameOverSound);
            anim.SetTrigger("Die");
            Debug.Log("Game over");
        }
        else
        {
            audioSource.PlayOneShot(damageSound);
            anim.SetTrigger("Hit");
        }
    }
    #endregion
}