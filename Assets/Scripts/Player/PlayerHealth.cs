using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerHealth : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    private HUDController hud;
    private Animator anim;

    [Header("Data Configuration")]
    [SerializeField] private PlayerDataSO data;

    [Header("Health Settings")]
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
    }

    private void Start()
    {
        currentHealth = data.maxHealth;
        // Warning: FindFirstObjectByType recorre toda la escena en runtime y acopla PlayerHealth a que exista un HUDController.
        // Serializa la referencia ([SerializeField] private HUDController hud;) o, mejor, expone un evento OnHealthChanged al que se suscriba el HUD (el HUD ya tiene esa suscripcion comentada).
        hud = FindFirstObjectByType<HUDController>();
        hud.UpdateHealthBar(currentHealth, data.maxHealth);
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
            // Suggestion: numero magico 20f. Pasalo a un campo serializado o al PlayerDataSO.
            TakeDamage(20f);
        }
    }

    // Error: TakeDamage es private y el unico que lo llama es HandleDebugInput con la tecla T. Ningun enemigo ni obstaculo le quita vida al jugador,
    // que es un requisito de la consigna. Hacelo public y llamalo desde un script en el enemigo u obstaculo (OnCollisionEnter2D u OnTriggerEnter2D con el Player).
    private void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        hud.UpdateHealthBar(currentHealth, data.maxHealth);

        // Bug: no hay guarda de muerte. Con currentHealth en 0, cada nueva llamada vuelve a entrar aca y repite gameOverSound y el trigger Die.
        // Agrega un bool isDead (o if (currentHealth <= 0) return; al principio del metodo) y deshabilita el input del jugador al morir.
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