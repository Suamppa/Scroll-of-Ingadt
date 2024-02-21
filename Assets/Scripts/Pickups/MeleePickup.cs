using System;

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
        WeaponDamage = UnityEngine.Random.Range(damageRangeMin, damageRangeMax);
        // Generate random attack speed between desired values
        WeaponAttackDelay = UnityEngine.Random.Range(attSpeedRangeMin, attSpeedRangeMax);

        base.OnEnable();
    }
}
