using UnityEngine;

public class Collectable : MonoBehaviour
{
    // This method is called when the collectable is picked up
    public virtual void OnPickup(Collider2D collector) {
        Debug.Log("Picked up " + gameObject.name);
        Destroy(gameObject);
    }

    // This method is called when something enters the collectable's trigger
    protected virtual void OnTriggerEnter2D(Collider2D other) {
        OnPickup(other);
    }
}
