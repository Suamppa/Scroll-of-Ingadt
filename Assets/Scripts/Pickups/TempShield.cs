using UnityEngine;

public class TempShield : TemporaryPickup
{
    // Number of shield charges to add
    public int shieldAmount = 2;

    public delegate void TempShieldEffect(float duration, int shieldAmount);
    public static event TempShieldEffect OnTempShieldApplied;
    public static event TempShieldEffect OnTempShieldRemoved;

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
        Debug.Log($"Initial shield is {collectorStats.shield}");
        collectorStats.shield += shieldAmount;
        Debug.Log($"Shield is now {collectorStats.shield}");
        OnTempShieldApplied?.Invoke(duration, shieldAmount);
    }

    public override void RemoveEffect(Stats collectorStats)
    {
        if (collectorStats.shield < shieldAmount) shieldAmount = collectorStats.shield;
        collectorStats.shield -= shieldAmount;
        Debug.Log($"Shield is now {collectorStats.shield}");
        if (collectorStats.shield < 1)
        {
            OnTempShieldRemoved?.Invoke(duration, shieldAmount);
        }
    }
}
