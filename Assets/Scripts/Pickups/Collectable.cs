using UnityEngine;

// Base class for collectables
public abstract class Collectable : MonoBehaviour
{
    public float dropChance = 0.5f;

    // This method is called when the collectable is picked up
    public virtual void OnPickup(Collider2D collector)
    {
        Destroy(gameObject);
    }

    // This method is called when something enters the collectable's trigger
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        OnPickup(other);
    }
}
