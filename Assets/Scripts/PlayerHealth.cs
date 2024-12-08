using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health = 100f; // Initial player health

    public void TakeDamage(float damage)
    {
        health -= damage; 
        Debug.Log($"Player took {damage} damage! Current health: {health}");

        if (health <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        
    }
}
