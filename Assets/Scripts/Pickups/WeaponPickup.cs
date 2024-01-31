using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : Collectable
{
    protected virtual void Awake()
    {        
        
    }

    public override void OnPickup(Collider2D collector)
    {
        Debug.Log($"{gameObject.name} picked up");

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        gameObject.transform.SetParent(collector.transform, false); 

        collector.GetComponent<Stats>().ChangeWeaponStats(this); 
        base.OnPickup(collector);
    }


    public virtual void ChangeWeapon(Stats collectorStats)
    {

    }
}
