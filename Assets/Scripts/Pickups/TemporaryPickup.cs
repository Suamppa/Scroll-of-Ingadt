using System.Collections;
using UnityEngine;

public abstract class TemporaryPickup : Collectable
{
    public float duration = 10f;

    public delegate void PickupEffect(float duration);
    public static event PickupEffect OnEffectApplied;
    public static event PickupEffect OnEffectRemoved;

    private SpriteRenderer spriteRenderer;

    protected virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // This method is called when the collectable is picked up
    public override void OnPickup(Collider2D collector)
    {
        Debug.Log($"{gameObject.name} picked up");
        // Disable sprite renderer and parent to collector
        spriteRenderer.enabled = false;
        gameObject.transform.SetParent(collector.transform);

        // Start coroutine to remove effect after duration
        collector.GetComponent<MonoBehaviour>().StartCoroutine(EffectActive(collector));
    }

    protected virtual IEnumerator EffectActive(Collider2D collector)
    {
        Stats collectorStats = collector.GetComponent<Stats>();
        
        ApplyEffect(collectorStats);
        Debug.Log($"{gameObject.name} effect started for {duration}.");
        yield return new WaitForSeconds(duration);
        Debug.Log($"{gameObject.name} effect ended.");
        RemoveEffect(collectorStats);
        base.OnPickup(collector);
    }

    public virtual void ApplyEffect(Stats collectorStats)
    {
        OnEffectApplied?.Invoke(duration);
    }

    public virtual void RemoveEffect(Stats collectorStats)
    {
        OnEffectRemoved?.Invoke(duration);
    }
}
