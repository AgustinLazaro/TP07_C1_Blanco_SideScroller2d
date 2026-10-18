using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    #region Variables
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 50f;
    [SerializeField] private float currentHealth;
    #endregion

    private void Start()
    {
        currentHealth = maxHealth;
    }
}