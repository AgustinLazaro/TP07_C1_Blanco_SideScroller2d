using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float currentHealth;

    private HUDController hud;

    private void Start()
    {
        currentHealth = maxHealth;
        hud = FindFirstObjectByType<HUDController>();
        hud.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(20f);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        hud.UpdateHealthBar(currentHealth, maxHealth);
    }
}