using System.Collections;
using UnityEngine;

public class TempShield : TemporaryPickup
{
    // Number of shield charges to add
    public int shieldAmount = 2;

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
        Debug.Log($"Initial shield is {collectorStats.shield}");
        collectorStats.shield += shieldAmount;
        Debug.Log($"Shield is now {collectorStats.shield}");
    }

    protected override void RemoveEffect(Stats collectorStats)
    {
        if (collectorStats.shield < shieldAmount) shieldAmount = collectorStats.shield;
        collectorStats.shield -= shieldAmount;
        Debug.Log($"Shield is now {collectorStats.shield}");
    }
}
