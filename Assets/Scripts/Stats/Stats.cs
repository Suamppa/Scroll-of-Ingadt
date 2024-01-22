using UnityEngine;

public class Stats : MonoBehaviour
{
    // Max health of the entity
    public int maxHealth = 6;
    // Current health of the entity
    public int currentHealth;
    // Movement speed of the entity
    public float moveSpeed = 10f;
    // Attack speed of the entity
    public float attackSpeed = 1f;
    // Damage dealt by the entity
    public int damage = 1;
    // Defense is subtracted from incoming damage
    public int defense = 0;
    // Shield prevents hits until depleted
    public int shield = 0;

    // Animator may be null for some entities
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        if (shield > 0)
        {
            ShieldDamage(damage);
        }
        else
        {
            HealthDamage(damage);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected void ShieldDamage(int damage)
    {
        shield--;
        Debug.Log($"{gameObject.name} blocked {damage} damage with shield. Shield is now {shield}");
    }

    // Take damage to health and return the amount of damage taken
    protected int HealthDamage(int damage)
    {
        int effectiveDamage = damage - defense;
        currentHealth -= effectiveDamage;
        if (animator != null)
        {
            animator.SetBool("isWounding", true);
        }
        Debug.Log($"{gameObject.name} took {damage} damage. Health is now {currentHealth}");
        return effectiveDamage;
    }

    // Handling death in a separate function allows for more flexibility to add e.g. death sounds
    public virtual void Die()
    {
        // Death sounds, animations, respawn logic etc. can go here
        Debug.Log($"{gameObject.name} died.");
        Destroy(gameObject);
    }

    public virtual void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Debug.Log($"{gameObject.name} healed {healAmount} health. Health is now {currentHealth}");
    }
}
