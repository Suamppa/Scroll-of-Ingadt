using UnityEngine;

public class EnemyStats : Stats
{
    private Rigidbody2D rb = null;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
    }

    public override void TakeDamage(int damage)
    {
        if (Shield > 0)
        {
            ShieldDamage(damage);
        }
        else
        {
            int effectiveDamage = HealthDamage(damage);
            rb.AddForce(-rb.velocity.normalized * effectiveDamage, ForceMode2D.Impulse);
        }
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    // Override the Die() function to add enemy death logic
    public override void Die()
    {
        if (TryGetComponent(out DropItem dropItem))
        {
            dropItem.Drop();
        }
        base.Die();
    }
}
