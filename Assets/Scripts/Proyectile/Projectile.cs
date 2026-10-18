using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Proyectile Settings")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 3f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        float direction = Mathf.Sign(transform.localScale.x);
        rb.linearVelocity = new Vector2(speed * direction, 0f);

        Destroy(gameObject, lifeTime);
    }
}
