using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Properties")]
    public float damage = 10f;
    public float range = 100f;

    [Header("Gun Components")]
    public Transform muzzlePoint;
    public LayerMask enemyLayer;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    void Update()
    {
        // Check if the shoot button (mouse left-click) is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Trigger muzzle flash effect
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        RaycastHit hit;

        // Debug ray visualization
        Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * range, Color.red, 1f);

        // Cast a ray from the muzzle point forward
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out hit, range, enemyLayer))
        {
            // Log the hit target
            Debug.Log($"Hit: {hit.transform.name}");

            // Deal damage to the enemy
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log($"Dealt {damage} damage to {hit.transform.name}");
            }

            // Spawn the impact effect at the hit point
            if (impactEffect != null)
            {
                Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
        else
        {
            Debug.Log("Missed! No object hit by the raycast.");
        }
    }
}
