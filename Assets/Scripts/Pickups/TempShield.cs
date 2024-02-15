using UnityEngine;

public class TempShield : TemporaryPickup
{
    // Number of shield charges to add
    public int shieldAmount = 2;

    public override void OnPickup(Collider2D collector)
    {
        TempShield existingShield = collector.GetComponentInChildren<TempShield>();
        // If the collector already has a shield, add time to it
        if (existingShield != null)
        {
            existingShield.Timer.AddTime(duration);
            Destroy(gameObject);
        }
        else
        {
            // Always call base.OnPickup() last to make sure the effect is applied
            base.OnPickup(collector);
        }
    }

    // This method is called when something enters the collectable's trigger
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Only pick up the collectable if the other object is the player
        if (other.CompareTag("Player"))
        {
            OnPickup(other);
        }
    }

    public override void ApplyEffect(Stats collectorStats)
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log($"Initial shield is {collectorStats.Shield}");
        }
        collectorStats.GainShield(shieldAmount);

        // Always call base.ApplyEffect() to start the timer
        base.ApplyEffect(collectorStats);
    }

    public override void RemoveEffect(Stats collectorStats)
    {
        collectorStats.ReduceShield(shieldAmount);

        // Always call base.RemoveEffect() to destroy the object
        base.RemoveEffect(collectorStats);
    }
}
