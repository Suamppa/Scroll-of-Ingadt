using System.Collections.Generic;
using UnityEngine;

// Based on the code created by @Ritumu
public class CanAttack : MonoBehaviour
{
    public int DamageAmount { get => attackerStats.damage; }
    public float AttackDelay { get => attackerStats.attackDelay; }

    // Layers that can be attacked
    public LayerMask[] targetLayers;
    // Reference to the attack detection collider
    public Collider2D attackCollider;
    // Reference to the user's own collider
    public Collider2D selfCollider;
    // Add Animator for animations
    public Animator animator;
    // Clips for hit and miss
    public AudioClip attackSound;

    // Filter for the attack detection collider
    private ContactFilter2D contactFilter;
    // How often can attack
    private Stats attackerStats;
    // Time of last attack
    private float lastAttackTime;
    private AudioSource audioSource;

    private void Awake()
    {
        // Set up the contact filter
        LayerMask layerMasks = new();
        foreach (LayerMask targetLayer in targetLayers)
        {
            layerMasks |= targetLayer;
        }
        contactFilter = new ContactFilter2D
        {
            useLayerMask = true,
            layerMask = layerMasks
        }; // This syntax is equivalent to setting the parameters row by row

        attackerStats = GetComponent<Stats>();

        audioSource = GetComponent<AudioSource>();
    }

    public void Attack()
    {
        // If time since last attack < attack delay, then don't attack
        if (Time.time - lastAttackTime < AttackDelay) return;
        animator.SetBool("IsAttacking", true);
        List<Collider2D> targets = new();
        // "_" means a discard, which means we don't care about the return value;
        // the function fills the targets list with the colliders in range
        _ = attackCollider.OverlapCollider(contactFilter, targets);
        // Damage all targets except the user's colliders and targets with the same tag as the user
        foreach (Collider2D target in targets)
        {
            if (target != attackCollider && target != selfCollider && !target.gameObject.CompareTag(tag))
            {
                PlayAudioAttack();
                try
                {
                    target.GetComponent<Stats>().TakeDamage(DamageAmount);
                }
                catch (System.NullReferenceException)
                {
                    Debug.LogWarning("Target " + target.name + " does not have a Stats component.");
                }
            }
        }
        // Mark this point as last time attacked
        lastAttackTime = Time.time;
    }

    public void PlayAudioAttack()
    {
        audioSource.clip = attackSound;
        audioSource.Play();
        Debug.Log(gameObject.name + " played attack sound");
    }

    // Draw the rough outline of the attack collider for debugging (not visible in game)
    void OnDrawGizmos()
    {
        // Set Gizmo color
        Gizmos.color = Color.red;

        // Draw a wire cube with the same position and size as our collider
        Gizmos.DrawWireCube(attackCollider.bounds.center, attackCollider.bounds.size);
    }
}
