using UnityEngine;

public class AttackBoost : TemporaryPickup
{
    // Boost is flat and additive
    public int damageBoost = 2;

    // This method is called when something enters the collectable's trigger
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Only pick up the collectable if the other object is the player
        if(other.CompareTag("Player")) {
            OnPickup(other);
        }
    }

    protected override void ApplyEffect(Stats collectorStats)
    {
        Debug.Log($"Initial damage is {collectorStats.damage}");
        collectorStats.damage += damageBoost;
        Debug.Log($"Damage is now {collectorStats.damage}");
    }

    protected override void RemoveEffect(Stats collectorStats)
    {
        collectorStats.damage -= damageBoost;
        Debug.Log($"Damage is now {collectorStats.damage}");
    }
}
