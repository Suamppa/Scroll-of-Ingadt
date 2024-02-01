using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MeleePickup : WeaponPickup
{
    // Weapons damage, the amount of damge weapon makes
    private int weaponDamage;
    // Weapon's attack speed (delay between each attack)
    private float weaponAttackDelay;
    // Attack delay changed to seconds for text field
    private float attackSpeedSeconds;
    // Min value for rng damage
    public int damageRangeMin = 2;
    // Max value for rng damage
    public int damageRangeMax = 4;
    // Min value for rng attack speed
    public float attSpeedRangeMin = 1.0f;
    // Max value for rng attack speed
    public float attSpeedRangeMax = 1.5f;

    protected override void Awake()
    {
        // Generate random attack damage between desired values
        weaponDamage = UnityEngine.Random.Range(damageRangeMin,damageRangeMax);
        // Generate random attack speed between desired values
        weaponAttackDelay = UnityEngine.Random.Range(attSpeedRangeMin, attSpeedRangeMax);
        // This will convert the attack speed to seconds (how many hits in second) and round it up
        attackSpeedSeconds = (float) Math.Round(1 / weaponAttackDelay, 2);
        // This will update the text under the pickup
        GetComponentInChildren<TextMeshPro>().SetText("Damage: " + weaponDamage + "\nSpeed: " + attackSpeedSeconds + "/s");      
    }


    public override void ChangeWeapon(Stats collectorStats)
    {
        Debug.Log($"Initial attackDelay is {collectorStats.attackDelay}");
        Debug.Log($"Initial damage is {collectorStats.damage}");

        // This will change attack speed
        collectorStats.attackDelay = weaponAttackDelay;
        // This will change damage
        collectorStats.damage = weaponDamage;
        
        Debug.Log($"attackDelay is now {collectorStats.attackDelay}");
        Debug.Log($"Damage is now {collectorStats.damage}");

        // Always call base.ApplyEffect() to start the timer
        base.ChangeWeapon(collectorStats);
    }
}
