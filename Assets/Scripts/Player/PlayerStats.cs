using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : Stats
{
    // Add Animator for animations
    public Animator animator;
    public delegate void PlayerDamaged(int newHealth);
    public static event PlayerDamaged OnPlayerDamaged;

    public override void TakeDamage(int damage)
    {
        int effectiveDamage = damage - defense;
        currentHealth -= effectiveDamage;
        animator.SetBool("isWounding", true);
        Debug.Log(gameObject.name + " took " + effectiveDamage + " damage. Health is now " + currentHealth);
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
        SceneManager.LoadScene("DeathScreen", LoadSceneMode.Single);
    }
}
