using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : Stats
{
    public delegate void PlayerHealthChanged(int newHealth);
    public static event PlayerHealthChanged OnPlayerDamaged;
    public static event PlayerHealthChanged OnPlayerHealed;

    public override void TakeDamage(int damage)
    {
        if (shield > 0)
        {
            ShieldDamage(damage);
        }
        else
        {
            HealthDamage(damage);
        }
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
        Debug.Log($"{gameObject.name} died.");
        // Add a game over screen here
        SceneManager.LoadScene("DeathScreen", LoadSceneMode.Single);
    }

    public override void Heal(int healAmount)
    {
        base.Heal(healAmount);
        // Invoke the OnPlayerHealed event
        OnPlayerHealed?.Invoke(currentHealth);
    }
}
