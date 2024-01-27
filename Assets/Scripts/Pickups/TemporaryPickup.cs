using UnityEngine;

public abstract class TemporaryPickup : Collectable
{
    public Timer Timer { get; private set; }
    
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
        // Disable sprite renderer and parent to collector
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        gameObject.transform.SetParent(collector.transform, false);

        // Go through this intermediary method to allow for overriding and special cases
        collector.GetComponent<Stats>().AddEffect(this);
    }

    public virtual void ApplyEffect(Stats collectorStats)
    {
        // Garbage collection will remove the event listener when the object is destroyed
        Timer.OnTimerEnd += () => RemoveEffect(collectorStats);
        Timer.StartTimer(duration);
    }

    public virtual void RemoveEffect(Stats collectorStats)
    {
        Destroy(gameObject);
    }
}
