using UnityEngine;

public class HealthRestore : Collectable
{
    // How much health the collectable can restore
    public int healthAmount = 2;

    // This method is called when the collectable is picked up
    public override void OnPickup(Collider2D collector)
    {
        Stats collectorStats = collector.GetComponent<Stats>();
        // Ignore collectable if collector is at max health
        if (collectorStats.currentHealth == collectorStats.maxHealth) return;
        Debug.Log($"{collector.name} picked up {gameObject.name}");

        // Restore health
        collectorStats.Heal(healthAmount);
        base.OnPickup(collector);
    }

    // This method is called when something enters the collectable's trigger
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Only pick up the collectable if the other object is the player
        if(other.CompareTag("Player")) {
            OnPickup(other);
        }
    }
}
