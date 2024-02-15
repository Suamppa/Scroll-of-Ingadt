using System;
using TMPro;
using UnityEngine;

public class MeleePickup : WeaponPickup
{
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
        WeaponDamage = UnityEngine.Random.Range(damageRangeMin,damageRangeMax);
        // Generate random attack speed between desired values
        WeaponAttackDelay = UnityEngine.Random.Range(attSpeedRangeMin, attSpeedRangeMax);
        // This will convert the attack speed to seconds (how many hits in second) and round it up
        AttackSpeedSeconds = (float) Math.Round(1 / WeaponAttackDelay, 2);

        base.OnEnable();
    }


    public override void ChangeWeapon(Stats collectorStats)
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

        base.ChangeWeapon(collectorStats);
    }
}
