using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : Stats
{
    public delegate void PlayerHealthChanged(int newHealth);
    public delegate void PlayerShieldChanged(int newShield);
    public delegate void PlayerStatusEffect<T>(T effect);
    public event PlayerHealthChanged OnPlayerDamaged;
    public event PlayerHealthChanged OnPlayerHealed;
    // public event PlayerShieldChanged OnPlayerShieldGained;
    public event PlayerShieldChanged OnPlayerShieldLost;
    public event PlayerStatusEffect<TemporaryPickup> OnPlayerStatus;
    public event PlayerStatusEffect<TempShield> OnPlayerTempShield;

    public override void TakeDamage(int damage)
    {
        if (Shield > 0)
        {
            ShieldDamage(damage);
        }
        else
        {
            HealthDamage(damage);
        }
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    protected override int HealthDamage(int damage)
    {
        int effectiveDamage = base.HealthDamage(damage);
        // Invoke the OnPlayerDamaged event
        OnPlayerDamaged?.Invoke(currentHealth);
        return effectiveDamage;
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

    public override void ReduceShield(int shieldAmount)
    {
        base.ReduceShield(shieldAmount);
        // Invoke the OnPlayerShieldLost event
        OnPlayerShieldLost?.Invoke(Shield);
    }

    public override void AddEffect(TemporaryPickup effect)
    {
        base.AddEffect(effect);
        if (effect is TempShield tempShield)
        {
            OnPlayerTempShield?.Invoke(tempShield);
        }
        else
        {
            OnPlayerStatus?.Invoke(effect);
        }
    }
}
