using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using JetBrains.Annotations;
using System.Runtime.CompilerServices;

public class SwordPickup : WeaponPickup
{
    // Weapons damage, the amount of damge weapon makes
    public int weaponDamage;
    // Weapon's attack speed (delay between each attack)
    public float weaponAttackDelay;

    public TextMeshPro weaponStats;

    private float attackSpeedSeconds;

    protected override void Awake()
    {
        
        weaponDamage = UnityEngine.Random.Range(2,3);
        weaponAttackDelay = UnityEngine.Random.Range(0.2f, 0.7f);
        attackSpeedSeconds = (float) Math.Round(1 / weaponAttackDelay, 2);
        weaponStats = GetComponentInChildren<TextMeshPro>();
        weaponStats.SetText("Damage: " + weaponDamage + "\nSpeed: " + attackSpeedSeconds + "/s");        
    }


    // This method is called when something enters the collectable's trigger
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        // Only pick up the collectable if the other object is the player
        if (other.CompareTag("Player"))
        {
            OnPickup(other);
        }
    }
    public override void ChangeWeapon(Stats collectorStats)
    {
        Debug.Log($"Initial attackDelay is {collectorStats.attackDelay}");
        Debug.Log($"Initial damage is {collectorStats.damage}");

        collectorStats.attackDelay = weaponAttackDelay;
        collectorStats.damage = weaponDamage;
        
        Debug.Log($"attackDelay is now {collectorStats.attackDelay}");
        Debug.Log($"Damage is now {collectorStats.damage}");

        // Always call base.ApplyEffect() to start the timer
        base.ChangeWeapon(collectorStats);
    }
}
