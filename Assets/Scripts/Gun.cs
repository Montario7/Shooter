using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Properties")]
    public float damage = 10f;         // Damage dealt by the gun
    public float range = 100f;        // Maximum shooting range

    [Header("Gun Components")]
    public Transform muzzlePoint;     // Gun's muzzle point for raycasting
    public LayerMask enemyLayer;      // Layer mask to ensure we only hit enemies
    public ParticleSystem muzzleFlash; // Muzzle flash particle system
    public GameObject impactEffect;   // Impact effect prefab for when the bullet hits a target

    [Header("Audio")]
    public AudioClip gunshotSound;    // Sound effect for the gunshot
    private AudioSource audioSource; // Audio source for playing sounds

    void Start()
    {
        // Ensure an AudioSource component exists on the gun
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on the gun. Sounds will not play.");
        }
    }

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
        // Play the muzzle flash particle effect
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }

        // Play gunshot sound
        if (audioSource != null && gunshotSound != null)
        {
            audioSource.PlayOneShot(gunshotSound);
        }

        RaycastHit hit;

        // Visualize the ray in the Scene view for debugging
        Debug.DrawRay(muzzlePoint.position, muzzlePoint.forward * range, Color.red, 1f);

        // Cast a ray from the muzzle point forward
        if (Physics.Raycast(muzzlePoint.position, muzzlePoint.forward, out hit, range, enemyLayer))
        {
            Debug.Log($"Hit: {hit.transform.name}"); // Log the name of the hit object

            // Check if the hit object has an EnemyAI component
            EnemyAI enemy = hit.transform.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage); // Deal damage to the enemy
                Debug.Log($"Dealt {damage} damage to {hit.transform.name}");
            }

            // Spawn the impact effect at the hit location
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
