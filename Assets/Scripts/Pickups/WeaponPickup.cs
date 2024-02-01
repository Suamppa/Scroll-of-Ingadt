using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponPickup : Collectable
{

    private Collider2D player;

    protected virtual void Awake()
    {        
        
    }

    public override void OnPickup(Collider2D collector)
    {
        if(player.gameObject.transform.childCount > 1)
        {
            DropWeaponInUse();
        }
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
        GameObject oldWeapon = player.transform.GetChild(1).gameObject;
        if(oldWeapon != null)
        {
            oldWeapon.transform.position = player.transform.position;
            oldWeapon.GetComponent<SpriteRenderer>().enabled = true;
            oldWeapon.GetComponent<Collider2D>().enabled = true;
            oldWeapon.GetComponentInChildren<TextMeshPro>().enabled = true;
            oldWeapon.transform.SetParent(null);
            Debug.Log("Dropped the used weapon");
             
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
            Debug.Log("Player can pickup weapon");
        }
    }
    // This will delete the reference to player when player leaves the weapons trigger
    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            // If the collider is the player, empty the variable
            player = null;
            Debug.Log("Player can't pickup weapon");
        }
        
    }



    public virtual void ChangeWeapon(Stats collectorStats)
    {

    }
}
