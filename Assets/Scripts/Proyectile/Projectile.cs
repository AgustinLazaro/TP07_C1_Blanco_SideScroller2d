using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AudioSource))]
public class Projectile : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    private Rigidbody2D rb;
    private AudioSource audioSource;

    [Header("Data Configuration")]
    [SerializeField] private ProjectileDataSO data;

    [Header("Audio Settings")]
    [SerializeField] private AudioClip slashSound;
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        InitializeProjectile();
    }
    #endregion

    #region Logic Methods
    private void InitializeProjectile()
    {
        // Warning: el sonido de disparo lo reproduce el AudioSource del propio proyectil, que se destruye al impactar (linea 50) o a los data.lifeTime segundos.
        // El clip se corta. Reproducilo desde el AudioSource del jugador en PlayerAttack o con AudioSource.PlayClipAtPoint.
        audioSource.PlayOneShot(slashSound);

        float direction = Mathf.Sign(transform.localScale.x);
        rb.linearVelocity = new Vector2(data.speed * direction, 0f);

        Destroy(gameObject, data.lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemy = collision.GetComponent<EnemyHealth>();

        // Suggestion: el proyectil solo se destruye contra enemigos; si toca el piso o un obstaculo lo atraviesa. Destruilo tambien al chocar con la capa Ground.
        if (enemy != null)
        {
            enemy.TakeDamage(data.damage);
            Destroy(gameObject);
        }
    }
    #endregion
}