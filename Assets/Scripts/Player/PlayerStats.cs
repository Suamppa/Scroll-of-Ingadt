using UnityEngine;

public class PlayerStats : Stats
{
    // Add Animator for animations
    public Animator animator;

    public override void TakeDamage(int damage)
    {
        int effectiveDamage = damage - defense;
        currentHealth -= effectiveDamage;
        animator.SetBool("isWounding", true);
        Debug.Log(gameObject.name + " took " + damage + " damage. Health is now " + currentHealth);
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
