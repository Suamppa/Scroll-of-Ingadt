using UnityEngine;

// This class represents a collectable with a timed effect
public abstract class TemporaryPickup : Collectable
{
    public Timer Timer { get; private set; }

    // Icon to pass to the collector
    public GameObject iconPrefab;
    public float duration = 10f;

    protected virtual void Awake()
    {
        if (iconPrefab == null)
        {
            Debug.LogError($"No icon prefab set for {gameObject.name}.");
        }
        Timer = gameObject.AddComponent<Timer>();
        Timer.SetTimer(duration);
    }

    // This method is called when the collectable is picked up
    public override void OnPickup(Collider2D collector)
    {
        Debug.Log($"{gameObject.name} picked up");

        // Pickup is hidden and moved to the collector for the duration of its effect
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        gameObject.transform.SetParent(collector.transform, false);

        // Go through this intermediary method to allow for overriding and special cases
        collector.GetComponent<Stats>().AddEffect(this);
    }

    // Override this method to apply the effect of the pickup
    public virtual void ApplyEffect(Stats collectorStats)
    {
        // Create an icon to represent the effect


        // Garbage collection will remove the event listener when the object is destroyed
        Timer.OnTimerEnd += () => RemoveEffect(collectorStats);
        Timer.StartTimer(duration);
    }

    // Override this method to remove the effect of the pickup
    public virtual void RemoveEffect(Stats collectorStats)
    {
        Destroy(gameObject);
    }
}
