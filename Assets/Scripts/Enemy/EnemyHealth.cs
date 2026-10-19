using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    private Animator anim;

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float currentHealth;
    #endregion

    private bool isDead;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Vida enemy: " + currentHealth);
        anim.SetTrigger("Hit");

        if (currentHealth <= 0)
        {
            Destroy(gameObject, 0.2f);
        }
    }
}