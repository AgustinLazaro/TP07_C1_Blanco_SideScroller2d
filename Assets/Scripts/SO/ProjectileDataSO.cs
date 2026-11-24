using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileDataSO", menuName = "Scriptable Objects/ProjectileDataSO")]
public class ProjectileDataSO : ScriptableObject
{
    [Header("Projectile Settings")]
    // Warning: speed, lifeTime y damage son public y mutables. Pasalos a [SerializeField] private con properties de solo lectura.
    public float speed = 10f;
    public float lifeTime = 3f;
    public float damage = 25f;
}
