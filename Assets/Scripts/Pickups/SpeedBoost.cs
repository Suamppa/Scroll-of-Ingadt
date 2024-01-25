using UnityEngine;

public class SpeedBoost : TemporaryPickup
{
    // Boost amount is flat and additive
    public float speedAmount = 5f;

    public delegate void SpeedBoostEffect(float duration, float speedAmount);
    public static event SpeedBoostEffect OnSpeedBoostApplied;
    public static event SpeedBoostEffect OnSpeedBoostRemoved;

    // This method is called when something enters the collectable's trigger
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Only pick up the collectable if the other object is the player
        if(other.CompareTag("Player")) {
            OnPickup(other);
        }
    }

    public override void ApplyEffect(Stats collectorStats)
    {
        Debug.Log($"Initial speed is {collectorStats.moveSpeed}");
        collectorStats.moveSpeed += speedAmount;
        Debug.Log($"Speed is now {collectorStats.moveSpeed}");
        OnSpeedBoostApplied?.Invoke(duration, speedAmount);
    }

    public override void RemoveEffect(Stats collectorStats)
    {
        collectorStats.moveSpeed -= speedAmount;
        Debug.Log($"Speed is now {collectorStats.moveSpeed}");
        OnSpeedBoostRemoved?.Invoke(duration, speedAmount);
    }
}
