using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Attack Settings")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }
    private void Attack()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        if (transform.localScale.x < 0)
        {
            Vector3 projectileScale = projectile.transform.localScale;
            projectileScale.x = projectileScale.x * -1;
            projectile.transform.localScale = projectileScale;
        }
    }
}
