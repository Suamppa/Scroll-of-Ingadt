using UnityEngine;

public class EnemyStats : Stats
{
    // Add Animator for animations
    public Animator animator;

    private Rigidbody2D rb = null;

    protected override void Start() {
        base.Start();
        moveSpeed = 5f;
        rb = GetComponent<Rigidbody2D>();
    }

    public override void TakeDamage(int damage)
    {
        int effectiveDamage = damage - defense;
        currentHealth -= effectiveDamage;
        rb.AddForce(-rb.velocity.normalized * effectiveDamage, ForceMode2D.Impulse);
        animator.SetBool("isWounding", true);
        Debug.Log(gameObject.name + " took " + damage + " damage. Health is now " + currentHealth);
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    // Override the Die() function to add enemy death logic
    public override void Die()
    {
        base.Die();
    }
}
