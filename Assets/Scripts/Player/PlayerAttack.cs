using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip slashSound;

    private Animator anim;
    private AudioSource audioSource;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
           
        }
    }

    private void Attack()
    {
        anim.SetTrigger("isAttacking");
        audioSource.PlayOneShot(slashSound);

        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        if (transform.localScale.x < 0)
        {
            Vector3 projectileScale = projectile.transform.localScale;
            projectileScale.x = projectileScale.x * -1;
            projectile.transform.localScale = projectileScale;
        }
    }
}
