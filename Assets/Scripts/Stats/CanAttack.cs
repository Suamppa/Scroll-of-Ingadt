using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanAttack : MonoBehaviour
{
    // Layers that can be attacked
    public LayerMask[] targetLayers;
    // Reference to the attack detection collider
    public Collider2D attackCollider;
    // Reference to the user's own collider
    public Collider2D selfCollider;

    // Filter for the attack detection collider
    private ContactFilter2D contactFilter;
    // How often can attack
    private float attackSpeed;
    // Damage amount
    private int damageAmount;
    // Time of last attack
    private float lastAttackTime;

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

        attackSpeed = GetComponent<Stats>().attackSpeed;
        damageAmount = GetComponent<Stats>().damage;
    }

    public void Attack()
    {
        // If time since last attack < attackSpeed, then don't attack
        if (Time.time - lastAttackTime < attackSpeed) return;

        List<Collider2D> targets = new();
        // "_" means a discard, which means we don't care about the return value
        _ = attackCollider.OverlapCollider(contactFilter, targets);

        // Damage all targets except the user's collider
        foreach (Collider2D target in targets)
        {
            if (target != attackCollider && target != selfCollider)
            {
                target.GetComponent<Stats>().TakeDamage(damageAmount);
            }
        }
        // Mark this point as last time attacked
        lastAttackTime = Time.time;
    }
}
