using System;
using UnityEngine;

public class Stats : MonoBehaviour
{
    // Current health of the entity
    public int CurrentHealth { get; private set; }
    // Max health of the entity
    public int MaxHealth { get { return baseHealth + bonusHealth; } }
    // Movement speed of the entity
    public float MoveSpeed { get { return baseMoveSpeed + bonusMoveSpeed; } }
    // Attack speed of the entity as the delay between attacks
    public float AttackDelay
    {
        get
        {
            float total = baseAttackDelay + bonusAttackDelay;
            if (equippedWeapon != null)
            {
                total += equippedWeapon.WeaponAttackDelay;
            }
            return Mathf.Max(total, 0f);
        }
    }
    public float AttackSpeedSeconds
    {
        get
        {
            // This will convert the attack speed to seconds (how many hits in second) and round it up
            return (float)Math.Round(1 / AttackDelay, 2);
        }
    }
    // Damage dealt by the entity
    public int Damage
    {
        get
        {
            int total = baseDamage + bonusDamage;
            if (equippedWeapon != null)
            {
                total += equippedWeapon.WeaponDamage;
            }
            return total;
        }
    }
    // Defense is subtracted from incoming damage
    public int Defense { get { return baseDefense + bonusDefense; } }
    // Shield prevents hits until depleted
    public int Shield { get { return baseShield + bonusShield; } }

    // Character data represents the base stats or "rules" of the entity
    public CharacterData characterData;

    // Bonus stats are added to the base stats
    // These can be modified by effects, items, etc.
    public int bonusHealth = 0;
    public float bonusMoveSpeed = 0f;
    // Negative values mean faster attacks, positive slower
    public float bonusAttackDelay = 0f;
    public int bonusDamage = 0;
    public int bonusDefense = 0;
    public int bonusShield = 0;

    // Base stats are set from the character data
    protected int baseHealth;
    protected float baseMoveSpeed;
    protected float baseAttackDelay;
    protected int baseDamage;
    protected int baseDefense;
    protected int baseShield;

    // Animator may be null for some entities
    protected Animator animator;
    // Reference to the equipped weapon, null if none
    protected WeaponPickup equippedWeapon;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        baseHealth = characterData.maxHealth;
        baseMoveSpeed = characterData.moveSpeed;
        baseAttackDelay = characterData.attackDelay;
        baseDamage = characterData.damage;
        baseDefense = characterData.defense;
        baseShield = characterData.shield;
    }

    protected virtual void OnEnable()
    {
        CurrentHealth = MaxHealth;
    }

    public virtual void ChangeWeapon()
    {
        string preMessage = $"Initial attackDelay is {AttackDelay}\nInitial damage is {Damage}";

        DropEquippedWeapon();
        equippedWeapon = GetComponentInChildren<WeaponPickup>();

        if (Debug.isDebugBuild)
        {
            Debug.Log(preMessage);
            Debug.Log($"attackDelay is now {AttackDelay}");
            Debug.Log($"Damage is now {Damage}");
        }

        if (equippedWeapon == null)
        {
            if (Debug.isDebugBuild)
            {
                Debug.Log("No weapon equipped");
            }
            animator.SetBool("isAxe", false);
            return;
        }

        Debug.Log($"Equipped weapon type: {equippedWeapon.weaponType}");

        switch (equippedWeapon.weaponType)
        {
            case WeaponPickup.WeaponType.Sword:
                animator.SetBool("isAxe", false);
                break;
            case WeaponPickup.WeaponType.Axe:
                animator.SetBool("isAxe", true);
                break;
            default:
                break;
        }
    }

    public virtual void DropEquippedWeapon()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.DropWeaponInUse();
            if (Debug.isDebugBuild)
            {
                Debug.Log($"Dropped {equippedWeapon.gameObject.name}");
            }
            equippedWeapon = null;
        }
    }

    public virtual void TakeDamage(int incomingDamage)
    {
        if (Shield > 0)
        {
            ShieldDamage(incomingDamage);
        }
        else
        {
            HealthDamage(incomingDamage);
        }
        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void ShieldDamage(int incomingDamage)
    {
        ReduceShield(1);

        if (Debug.isDebugBuild)
        {
            Debug.Log($"{gameObject.name} blocked {incomingDamage} damage with shield. Shield is now {Shield}");
        }
    }

    // Take damage to health and return the amount of damage taken
    protected virtual int HealthDamage(int incomingDamage)
    {
        int effectiveDamage = incomingDamage - Defense;
        CurrentHealth -= effectiveDamage;
        if (animator != null)
        {
            animator.SetTrigger("Wounding");
        }

        if (Debug.isDebugBuild)
        {
            Debug.Log($"{gameObject.name} took {incomingDamage} damage. Health is now {CurrentHealth}");
        }

        return effectiveDamage;
    }

    // Handling death in a separate function allows for more flexibility to add e.g. death sounds
    public virtual void Die()
    {
        // Death sounds, animations, respawn logic etc. can go here
        if (Debug.isDebugBuild)
        {
            Debug.Log($"{gameObject.name} died.");
        }

        Destroy(gameObject);
    }

    public virtual void Heal(int healAmount)
    {
        CurrentHealth += healAmount;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        if (Debug.isDebugBuild)
        {
            Debug.Log($"{gameObject.name} healed {healAmount} health. Health is now {CurrentHealth}");
        }
    }

    public virtual void GainShield(int shieldAmount)
    {
        bonusShield += shieldAmount;

        if (Debug.isDebugBuild)
        {
            Debug.Log($"{gameObject.name} gained {shieldAmount} shield. Shield is now {Shield}");
        }
    }

    public virtual void ReduceShield(int reduceAmount)
    {
        if (bonusShield >= reduceAmount)
        {
            bonusShield -= reduceAmount;
        }
        else
        {
            int remainingAmount = reduceAmount - bonusShield;
            bonusShield = 0;
            if (baseShield > remainingAmount)
            {
                baseShield -= remainingAmount;
            }
            else
            {
                baseShield = 0;
            }
        }

        if (Debug.isDebugBuild)
        {
            Debug.Log($"{gameObject.name} lost {reduceAmount} shield. Shield is now {Shield}");
        }
    }

    // Overload this method to add handling for different effect types
    public virtual void AddEffect(TemporaryPickup pickup)
    {
        // Special handling/conditionals can go here

        // Apply the effect
        pickup.ApplyEffect(this);
    }
}
