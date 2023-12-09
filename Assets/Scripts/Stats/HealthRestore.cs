using UnityEngine;

public class HealthRestore : Collectable
{
    // How much health the collectable can restore
    public int healthAmount = 1;

    // This method is called when the collectable is picked up
    public override void OnPickup(Collider2D collector)
    {
        collector.GetComponent<Stats>().Heal(healthAmount);
        base.OnPickup(collector);
    }

    // This method is called when something enters the collectable's trigger
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) {
            OnPickup(other);
        }
    }
}
