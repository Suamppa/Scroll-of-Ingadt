using System;
using TMPro;
using UnityEngine;

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

    protected override void OnEnable()
    {
        // Generate random attack damage between desired values
        weaponDamage = UnityEngine.Random.Range(damageRangeMin,damageRangeMax);
        // Generate random attack speed between desired values
        weaponAttackDelay = UnityEngine.Random.Range(attSpeedRangeMin, attSpeedRangeMax);
        // This will convert the attack speed to seconds (how many hits in second) and round it up
        attackSpeedSeconds = (float) Math.Round(1 / weaponAttackDelay, 2);
        // This will update the text under the pickup
        GetComponentInChildren<TextMeshPro>().SetText($"Damage: {weaponDamage}\nSpeed: {attackSpeedSeconds}/s");
    }


    public override void ChangeWeapon(Stats collectorStats)
    {
        string preMessage = $"Initial attackDelay is {collectorStats.AttackDelay}\nInitial damage is {collectorStats.Damage}";

        // This will change attack speed
        collectorStats.bonusAttackDelay = weaponAttackDelay;
        // This will change damage
        collectorStats.bonusDamage = weaponDamage;
        
        if (Debug.isDebugBuild)
        {
            Debug.Log(preMessage);
            Debug.Log($"attackDelay is now {collectorStats.AttackDelay}");
            Debug.Log($"Damage is now {collectorStats.Damage}");
        }

        base.ChangeWeapon(collectorStats);
    }
}
