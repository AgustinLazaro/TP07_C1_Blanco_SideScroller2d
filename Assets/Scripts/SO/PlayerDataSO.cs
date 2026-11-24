using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDataSO", menuName = "Scriptable Objects/PlayerDataSO")]
public class PlayerDataSO : ScriptableObject
{
    [Header("Movement Settings")]
    // Warning: todos los campos de este SO son public y mutables (moveSpeed, jumpForce, crouchSpeed, maxHealth, projectilePrefab).
    // Pasalos a [SerializeField] private y expone properties de solo lectura para que los MonoBehaviours no puedan modificar el asset.
    public float moveSpeed = 9f;
    public float jumpForce = 12f;
    public float crouchSpeed = 0.5f;

    [Header("Health Settings")]
    public float maxHealth = 100f;

    [Header("Attack Settings")]
    public GameObject projectilePrefab;
}
