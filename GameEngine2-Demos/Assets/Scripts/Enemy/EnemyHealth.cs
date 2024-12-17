using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // Initialize the health to maxHealth when the script starts
    private void Awake()
    {
        currentHealth = maxHealth;
    }

    // Function to take damage
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " took " + damage + " damage! Remaining health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Function that handles death
    private void Die()
    {
        Debug.Log(gameObject.name + " has died!");
        Destroy(gameObject); // Destroy the enemy game object
    }

    // Optionally, you can expose a function to check health if needed
    public bool IsDead()
    {
        return currentHealth <= 0;
    }
}
