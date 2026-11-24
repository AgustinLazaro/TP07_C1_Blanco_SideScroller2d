using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDataSO", menuName = "Scriptable Objects/EnemyDataSO")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Health Settings")]
    // Warning: campo public mutable. Cualquier script puede cambiar el valor del asset en runtime y en el editor ese cambio persiste.
    // Usa [SerializeField] private float maxHealth; con una property de solo lectura public float MaxHealth => maxHealth;
    public float maxHealth = 50f;
}
