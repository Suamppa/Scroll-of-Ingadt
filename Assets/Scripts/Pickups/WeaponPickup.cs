using TMPro;
using UnityEngine;

public class WeaponPickup : Collectable
{
    protected Collider2D player;

    // Weapons damage, the amount of damge weapon makes
    public int WeaponDamage { get; protected set; }
    // Weapon's attack speed (delay between each attack)
    public float WeaponAttackDelay { get; protected set; }
    // Attack delay changed to seconds for text field
    public float AttackSpeedSeconds { get; protected set; }

    // Icon to pass to the collector
    public GameObject iconPrefab;

    protected virtual void OnEnable()
    {
        if (Debug.isDebugBuild && iconPrefab == null)
        {
            Debug.LogError($"No icon prefab set for {gameObject.name}.");
        }
        // This will update the text under the pickup
        GetComponentInChildren<TextMeshPro>().SetText($"Damage: {WeaponDamage}\nSpeed: {AttackSpeedSeconds}/s");
    }

    public override void OnPickup(Collider2D collector)
    {
        if (Debug.isDebugBuild)
        {
            Debug.Log($"{gameObject.name} picked up");
        }

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<TextMeshPro>().enabled = false;
        gameObject.transform.SetParent(collector.transform, false);

        collector.GetComponent<Stats>().ChangeWeapon();
    }

    public void DropWeaponInUse()
    {
        gameObject.transform.position = player.transform.position;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<Collider2D>().enabled = true;
        gameObject.GetComponentInChildren<TextMeshPro>().enabled = true;
        gameObject.transform.SetParent(null);
    }

    public void PlayerPickupWeapon()
    {
        if (player != null) OnPickup(player);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // If the collider is the player, set variable to player
            player = other;
        }
    }

    // This will delete the reference to player when player leaves the weapons trigger
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // If the collider is the player, empty the variable
            player = null;
        }

    }

    public virtual void ChangeWeapon(Stats collectorStats)
    {
        string preMessage = $"Initial attackDelay is {collectorStats.AttackDelay}\nInitial damage is {collectorStats.Damage}";

        // This will change attack speed
        collectorStats.bonusAttackDelay = WeaponAttackDelay;
        // This will change damage
        collectorStats.bonusDamage = WeaponDamage;

        if (Debug.isDebugBuild)
        {
            Debug.Log(preMessage);
            Debug.Log($"attackDelay is now {collectorStats.AttackDelay}");
            Debug.Log($"Damage is now {collectorStats.Damage}");
        }
    }
}
