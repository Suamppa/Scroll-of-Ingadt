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
        DropWeaponInUse();
        
        if (Debug.isDebugBuild)
        {
            Debug.Log($"{gameObject.name} picked up");
        }

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<TextMeshPro>().enabled = false;
        gameObject.transform.SetParent(collector.transform, false);

        collector.GetComponent<Stats>().ChangeWeaponStats(this);
    }

    public void DropWeaponInUse()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject oldWeapon;
        foreach (Transform t in player.GetComponentInChildren<Transform>())
        {
            if (t.CompareTag("Weapon"))
            {
                oldWeapon = t.gameObject;
                oldWeapon.transform.position = player.transform.position;
                oldWeapon.GetComponent<SpriteRenderer>().enabled = true;
                oldWeapon.GetComponent<Collider2D>().enabled = true;
                oldWeapon.GetComponentInChildren<TextMeshPro>().enabled = true;
                oldWeapon.transform.SetParent(null);

                if (Debug.isDebugBuild)
                {
                    Debug.Log("Dropped the used weapon");
                }
            }
        }
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
        return;
    }
}
