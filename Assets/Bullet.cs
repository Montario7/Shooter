using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 3f;

    void Awake()
    {
        Destroy(gameObject, life); // Automatically destroy the bullet after a certain time
    }

    void OnCollisionEnter(Collision collision)
    {
        // Only affect objects with the "Enemy" tag
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the enemy or reduce its health here
            Destroy(collision.gameObject);
        }

        // Destroy the bullet in all cases after collision
        Destroy(gameObject);
    }
}
