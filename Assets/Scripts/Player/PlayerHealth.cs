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
       
    }
}