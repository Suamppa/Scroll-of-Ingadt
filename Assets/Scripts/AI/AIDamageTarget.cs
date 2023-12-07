using UnityEngine;

public class AIDamageTarget : MonoBehaviour
{
    public PlayerStats playerStats;
    // How often can attack
    public float attackInterval = 1; // Deprecated

    // Damage amount
    private int damageAmount;
    private float attackSpeed;
    private float lastAttackTime;

    private void Awake() {
        // Unity is able to find the EnemyStats component due to inheritance
        attackSpeed = GetComponent<Stats>().attackSpeed;
        damageAmount = GetComponent<Stats>().damage;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // If time since last attack < attackSpeed, then don't attack
        if(Time.time - lastAttackTime < attackSpeed) return;

        if(collision.gameObject.CompareTag("Player"))
        {
            // Damage player
            playerStats.TakeDamage(damageAmount);

            // Mark this point as last time attacked
            lastAttackTime = Time.time;
        }
    }
}
