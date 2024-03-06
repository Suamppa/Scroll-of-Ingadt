using System;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int DamageAmount { get => attackerStats.Damage; }
    public float AttackDelay { get => attackerStats.AttackDelay; }
    //This is how much the laser will damage entities
    public int LaserDamageAmount = 1;
    // This is how many times in a second the laser will damage entities
    public float LaserAttackDPS = 0.5f;
    // How often boss can do laser attack
    public float LaserAttackDelay = 5.0f;
    public float LaserAttackRange = 7.0f;


    public static float laserAttackDuration = 1.5f;    // Reference to the attack detection collider (Boss currently doesn't use this)
    public CapsuleCollider2D AttackCollider { get; set; }
    // Reference to the user's own collider
    public BoxCollider2D SelfCollider { get; set; }

    // Layers that can be attacked
    public LayerMask[] targetLayers;
    // Add Animator for animations
    public Animator animator;
    // Clips for hit and miss
    public AudioClip attackSound;

    public Timer Timer { get; private set; }

    // Filter for the attack detection collider
    private ContactFilter2D contactFilter;
    // How often can attack
    private Stats attackerStats;
    // Time of last attack
    private float lastAttackTime;

    private float lastMeleeAttackTime;
    private AudioSource audioSource;

    private bool isAttacking = false;


    Rigidbody2D rb2D;

    private RaycastHit2D hitDown;
    private RaycastHit2D hitLeft;
    private RaycastHit2D hitUp;
    private RaycastHit2D hitRight;
    private RaycastHit2D[] hitList;

    public LineRenderer lineRendererUp;
    public LineRenderer lineRendererRight;
    public LineRenderer lineRendererDown;
    public LineRenderer lineRendererLeft;

    private void Awake()
    {
        SelfCollider = GetComponent<BoxCollider2D>();
        AttackCollider = GetComponentInChildren<CapsuleCollider2D>();

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

    private void OnEnable()
    {
        AttackCollider = GetComponentInChildren<CapsuleCollider2D>();

        Timer = gameObject.AddComponent<Timer>();

    }

    void FixedUpdate()
    {
        LaserAttackUpdate();
    }

    private void LaserAttackUpdate()
    {
        if (Time.time - lastAttackTime < LaserAttackDPS) return;

        if (isAttacking)
        {
            hitList = new RaycastHit2D[] { hitUp, hitRight, hitDown, hitLeft };

            hitUp = Physics2D.CircleCast(transform.Find("LineRenderer up").transform.position, 1.0f, Vector2.up, LaserAttackRange);

            hitRight = Physics2D.CircleCast(transform.Find("LineRenderer right").transform.position, 1.0f, Vector2.right, LaserAttackRange);

            hitDown = Physics2D.CircleCast(transform.Find("LineRenderer down").transform.position, 1.0f, Vector2.down, LaserAttackRange);

            hitLeft = Physics2D.CircleCast(transform.Find("LineRenderer left").transform.position, 1.0f, Vector2.left, LaserAttackRange);


            foreach (RaycastHit2D hit in hitList)
            {
                if (hit.collider != null)
                {
                    lastAttackTime = Time.time;
                    if (Debug.isDebugBuild)
                    {
                        Debug.Log("Boss is trying to attack");
                        Debug.Log("LineRenderer is: " + hit.transform.name);
                    }
                    PlayAudioAttack();
                    try
                    {
                        hit.collider.GetComponent<Stats>().TakeDamage(LaserDamageAmount);
                    }
                    catch (NullReferenceException)
                    {
                        if (Debug.isDebugBuild)
                        {
                            Debug.LogWarning("Target " + hit.collider.name + " does not have a Stats component.");
                        }
                    }
                }
            }

            if (hitUp)
            {
                DrawLaser(lineRendererUp, Vector2.zero, transform.up * hitUp.distance + new Vector3(0.0f, 0.5f));
            }
            else
            {
                DrawLaser(lineRendererUp, Vector2.zero, transform.up * LaserAttackRange);
            }

            if (hitRight)
            {
                DrawLaser(lineRendererRight, Vector2.zero, transform.right * hitRight.distance + new Vector3(0.5f, 0.0f));
            }
            else
            {
                DrawLaser(lineRendererRight, Vector2.zero, transform.right * LaserAttackRange);
            }

            if (hitDown)
            {
                DrawLaser(lineRendererDown, Vector2.zero, -transform.up * hitDown.distance + new Vector3(0.0f,-0.5f));
            }
            else
            {
                DrawLaser(lineRendererDown, Vector2.zero, -transform.up * LaserAttackRange);
            }

            if (hitLeft)
            {
                DrawLaser(lineRendererLeft, Vector2.zero, -transform.right * hitLeft.distance + new Vector3(-0.5f, 0.0f));
            }
            else
            {
                DrawLaser(lineRendererLeft, Vector2.zero, -transform.right * LaserAttackRange);
            }
        }
    }

    public void LaserAttack()
    {
        // If time since last attack < attack delay, then don't attack
        if (Time.time - lastAttackTime < LaserAttackDelay) return;

        Timer.OnTimerEnd += () =>
        {
            RemoveLaser(lineRendererUp);
            RemoveLaser(lineRendererRight);
            RemoveLaser(lineRendererDown);
            RemoveLaser(lineRendererLeft);
            lineRendererUp.enabled = false;
            lineRendererRight.enabled = false;
            lineRendererDown.enabled = false;
            lineRendererLeft.enabled = false;

            isAttacking = false;
        };

        Timer.StartTimer(laserAttackDuration);

        lineRendererUp.enabled = true;
        lineRendererRight.enabled = true;
        lineRendererDown.enabled = true;
        lineRendererLeft.enabled = true;

        isAttacking = true;

        // Trigger the attack animation
        animator.SetTrigger("Attacking");

        // Mark this point as last time attacked

    }

    public void Attack()
    {
        // If time since last attack < attack delay, then don't attack
        if (Time.time - lastMeleeAttackTime < AttackDelay) return;
        // Trigger the attack animation
        animator.SetTrigger("Attacking");
        List<Collider2D> targets = new();
        // "_" means a discard, which means we don't care about the return value;
        // the function fills the targets list with the colliders in range
        _ = AttackCollider.OverlapCollider(contactFilter, targets);
        // Damage all targets except the user's colliders and targets with the same tag as the user
        foreach (Collider2D target in targets)
        {
            if (target != AttackCollider && target != SelfCollider && !target.gameObject.CompareTag(tag))
            {
                PlayAudioAttack();
                try
                {
                    target.GetComponent<Stats>().TakeDamage(DamageAmount);
                }
                catch (System.NullReferenceException)
                {
                    if (Debug.isDebugBuild)
                    {
                        Debug.LogWarning("Target " + target.name + " does not have a Stats component.");
                    }
                }
            }
        }
        // Mark this point as last time attacked
        lastMeleeAttackTime = Time.time;
    }



    void DrawLaser(LineRenderer lineRenderer, Vector2 startPos, Vector2 endPos)
    {
        lineRenderer.SetPosition(0, startPos);
        lineRenderer.SetPosition(1, endPos);
    }

    void RemoveLaser(LineRenderer lineRenderer)
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
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
        if (AttackCollider == null) return;

        // Set Gizmo color
        Gizmos.color = Color.red;

        // Draw a wire cube with the same position and size as our collider
        Gizmos.DrawWireCube(AttackCollider.bounds.center, AttackCollider.bounds.size);
    }

}
