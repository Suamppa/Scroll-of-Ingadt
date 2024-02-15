using UnityEngine;

public class AttackBoost : TemporaryPickup
{
    // Boost is flat and additive
    public int damageBoost = 2;

    public override void OnPickup(Collider2D collector)
    {
        AttackBoost existingBoost = collector.GetComponentInChildren<AttackBoost>();
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
        string preMessage = $"Initial damage is {collectorStats.Damage}";
        collectorStats.bonusDamage += damageBoost;

        if (Debug.isDebugBuild)
        {
            Debug.Log(preMessage);
            Debug.Log($"Damage is now {collectorStats.Damage}");
        }

        // Always call base.ApplyEffect() to start the timer
        base.ApplyEffect(collectorStats);
    }

    public override void RemoveEffect(Stats collectorStats)
    {
        collectorStats.bonusDamage -= damageBoost;
        
        if (Debug.isDebugBuild)
        {
            Debug.Log($"Damage is now {collectorStats.Damage}");
        }

        // Always call base.RemoveEffect() to destroy the object
        base.RemoveEffect(collectorStats);
    }
}
