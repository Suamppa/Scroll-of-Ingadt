using TMPro;
using UnityEngine;

public class WeaponPickup : Collectable
{

    private Collider2D player;

    // Icon to pass to the collector
    public GameObject iconPrefab;

    protected virtual void Awake()
    {        
        if (iconPrefab == null)
        {
            Debug.LogError($"No icon prefab set for {gameObject.name}.");
        }
    }

    public override void OnPickup(Collider2D collector)
    {
        DropWeaponInUse();
        Debug.Log($"{gameObject.name} picked up");

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponentInChildren<TextMeshPro>().enabled = false;
        gameObject.transform.SetParent(collector.transform, true); 

        collector.GetComponent<Stats>().ChangeWeaponStats(this); 
    }

    public void DropWeaponInUse()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject oldWeapon;
        foreach(Transform t in player.GetComponentInChildren<Transform>())
        {
            if(t.CompareTag("Weapon"))
            {
                oldWeapon = t.gameObject;
                oldWeapon.transform.position = player.transform.position;
                oldWeapon.GetComponent<SpriteRenderer>().enabled = true;
                oldWeapon.GetComponent<Collider2D>().enabled = true;
                oldWeapon.GetComponentInChildren<TextMeshPro>().enabled = true;
                oldWeapon.transform.SetParent(null);
                Debug.Log("Dropped the used weapon");
            }
        }
    }

    public void PlayerPickupWeapon()
    {
        OnPickup(player);
    }
    // This will set 
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // If the collider is the player, set variable to player
            player = other;
        }
    }
    // This will delete the reference to player when player leaves the weapons trigger
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // If the collider is the player, empty the variable
            player = null;
        }
        
    }
    
    public virtual void ChangeWeapon(Stats collectorStats)
    {

    }
}
