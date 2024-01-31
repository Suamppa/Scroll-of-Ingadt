using UnityEngine;

public class SpeedBoost : TemporaryPickup
{
    // Boost amount is flat and additive
    public float moveSpeedAmount = 5f;
    public float attackDelayReduction = 0.5f;

    public override void OnPickup(Collider2D collector)
    {
        SpeedBoost existingBoost = collector.GetComponentInChildren<SpeedBoost>();
        // If the collector already has this boost, add time to it
        if (existingBoost != null)
        {
            existingBoost.Timer.AddTime(duration);
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
        Debug.Log($"Initial move speed is {collectorStats.moveSpeed}");
        Debug.Log($"Initial attack delay is {collectorStats.attackDelay}");

        collectorStats.moveSpeed += moveSpeedAmount;
        collectorStats.attackDelay -= attackDelayReduction;

        Debug.Log($"Speed is now {collectorStats.moveSpeed}");
        Debug.Log($"Attack delay is now {collectorStats.attackDelay}");

        // Always call base.ApplyEffect() to start the timer
        base.ApplyEffect(collectorStats);
    }

    public override void RemoveEffect(Stats collectorStats)
    {
        collectorStats.moveSpeed -= moveSpeedAmount;
        collectorStats.attackDelay += attackDelayReduction;

        Debug.Log($"Speed is now {collectorStats.moveSpeed}");
        Debug.Log($"Attack delay is now {collectorStats.attackDelay}");

        // Always call base.RemoveEffect() to destroy the object
        base.RemoveEffect(collectorStats);
    }
}
