using System.Collections;
using UnityEngine;

public class SpeedBoost : TemporaryPickup
{
    // Boost amount is flat and additive
    public float speedAmount = 5f;

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
        Debug.Log($"Initial speed is {collectorStats.moveSpeed}");
        collectorStats.moveSpeed += speedAmount;
        Debug.Log($"Speed is now {collectorStats.moveSpeed}");
    }

    protected override void RemoveEffect(Stats collectorStats)
    {
        collectorStats.moveSpeed -= speedAmount;
        Debug.Log($"Speed is now {collectorStats.moveSpeed}");
    }
}
