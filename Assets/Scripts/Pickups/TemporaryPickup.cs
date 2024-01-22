using System.Collections;
using UnityEngine;

public abstract class TemporaryPickup : Collectable
{
    public float duration = 10f;

    private SpriteRenderer spriteRenderer;

    protected abstract void ApplyEffect(Stats collectorStats);
    protected abstract void RemoveEffect(Stats collectorStats);

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
}
