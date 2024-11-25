using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; // Initial player health

    public void TakeDamage(float damage)
    {
        health -= damage; // Reduce health
        Debug.Log($"Player took {damage} damage! Current health: {health}");

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        // Add logic for when the player dies (e.g., restart the level)
    }
}
