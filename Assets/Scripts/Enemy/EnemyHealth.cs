using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    private Animator anim;
    private Collider2D col;

    [Header("Data Configuration")]
    [SerializeField] private EnemyDataSO data;

    [Header("Effects")]
    [SerializeField] private GameObject deathEffectPrefab;

    [Header("Audio Settings")]
    // Suggestion: audioSource es [SerializeField] pero en Awake (linea 28) se pisa con GetComponent, asi que lo que asignes en el inspector se ignora.
    // Elegi una sola fuente: o lo dejas serializado y sacas el GetComponent, o lo haces private sin [SerializeField] y agregas [RequireComponent(typeof(AudioSource))].
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip deathSound;

    [Header("Health Settings")]
    [SerializeField] private float currentHealth;
    #endregion

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        col = GetComponent<Collider2D>();
    }

    private void Start()
    {
        currentHealth = data.maxHealth;
    }

    // Bug: no hay guarda para un enemigo que ya murio. Si dos proyectiles entran en el mismo paso de fisica, col.enabled = false no frena el segundo OnTriggerEnter2D,
    // y se ejecuta dos veces la rama de muerte (dos PlayOneShot, dos Instantiate de particulas, dos Destroy). Agrega un bool isDead y hace return al principio si ya esta muerto.
    public void TakeDamage(float damageAmount)
    {
        // Error: la consigna pide que la vida se visualice en una barra arriba del enemigo (Canvas World Space). No hay ningun Slider ni Canvas en Enemy.prefab
        // y este metodo no notifica a ninguna barra. Agrega un Canvas en World Space hijo del enemigo con un Slider y actualizalo aca con currentHealth / data.maxHealth.
        currentHealth -= damageAmount;
        Debug.Log("Vida enemy: " + currentHealth);

        if (currentHealth > 0)
        {
            // Warning: string magico "Hit" repetido (tambien en la linea 51). Si renombras el parametro en EnemyController.controller falla en silencio.
            // Declara private static readonly int HitHash = Animator.StringToHash("Hit"); y usalo en las dos llamadas.
            anim.SetTrigger("Hit");
            audioSource.PlayOneShot(damageSound);
        }
        else
        {
            col.enabled = false;

            anim.SetTrigger("Hit");
            audioSource.PlayOneShot(deathSound);
            Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);

            Destroy(gameObject, deathSound.length);
        }
    }
}