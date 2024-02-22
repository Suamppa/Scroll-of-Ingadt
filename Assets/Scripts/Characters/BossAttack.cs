using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int DamageAmount { get => attackerStats.Damage; }
    public float AttackDelay { get => attackerStats.AttackDelay; }

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

    Rigidbody2D rb2D;

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

        rb2D = GetComponent<Rigidbody2D>();
    }

    public virtual void Attack()
    {
        // If time since last attack < attack delay, then don't attack
        if (Time.time - lastAttackTime < AttackDelay) return;

        LayerMask mask = LayerMask.GetMask("Damageable");

        // Trigger the attack animation
        animator.SetTrigger("Attacking");


        GameObject target = GetComponent<BossChase>().target;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 1.0f, (target.transform.position - transform.position).normalized, 10.0f, mask);


        
        

        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Player"))
            {
                
                if(Debug.isDebugBuild)
                {
                    Debug.Log("Boss is trying to attack");
                }
                PlayAudioAttack();
                try
                {
                    hit.collider.GetComponent<Stats>().TakeDamage(DamageAmount);
                    Debug.DrawLine(transform.position, target.transform.position);
                    
                }
                catch (System.NullReferenceException)
                {
                    if (Debug.isDebugBuild)
                    {
                        Debug.LogWarning("Target " + hit.collider.name + " does not have a Stats component.");
                    }
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
        
        if (Debug.isDebugBuild)
        {
            Debug.Log(gameObject.name + " played attack sound");
        }
    }

    // Draw the rough outline of the attack collider for debugging (not visible in game)
    private void OnDrawGizmos()
    {
        // Set Gizmo color
        Gizmos.color = Color.red;

        // Draw a wire cube with the same position and size as our collider
        Gizmos.DrawWireCube(attackCollider.bounds.center, attackCollider.bounds.size);

    }

}
