using UnityEngine;
using UnityEngine.Audio;

public class Projectile : MonoBehaviour
{
    [Header("Proyectile Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private AudioClip slashSound;

    private Rigidbody2D rb;
    private AudioSource audioSource;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        audioSource.PlayOneShot(slashSound);
        float direction = Mathf.Sign(transform.localScale.x);
        rb.linearVelocity = new Vector2(speed * direction, 0f);
        
        Destroy(gameObject, lifeTime);
    }
}
