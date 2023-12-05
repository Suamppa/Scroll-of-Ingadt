using System.Collections;
using System.Collections.Generic;
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
    // Defense of the entity
    public int defense = 0;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage - defense;
        Debug.Log(gameObject.name + " took " + damage + " damage. Health is now " + currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    // Handling death in a separate function allows for more flexibility to add e.g. death sounds
    public virtual void Die()
    {
        // Death sounds, animations, respawn logic etc. can go here
        Debug.Log(gameObject.name + " died.");
        Destroy(gameObject);
    }
}
