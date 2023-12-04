using JetBrains.Annotations;
using UnityEngine;

public class PlayerStats : Stats
{
    public delegate void PlayerDamaged(int newHealth);
    public static event PlayerDamaged OnPlayerDamaged;

    public override void TakeDamage(int damage)
    {
        currentHealth -= damage - defense;
        Debug.Log(gameObject.name + " took " + damage + " damage. Health is now " + currentHealth);
        // Invoke the OnPlayerDamaged event
        OnPlayerDamaged?.Invoke(currentHealth);
        
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    // Override the Die() function to add a game over screen
    public override void Die()
    {
        // Death sounds, animations, respawn logic etc. can go here
        Debug.Log(gameObject.name + " died.");
        // Add a game over screen here
        Destroy(gameObject);
    }
}
