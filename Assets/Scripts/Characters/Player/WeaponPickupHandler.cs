using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupHandler : MonoBehaviour
{
    // Collider of the weapon that has been encountered on the ground
    private Collider2D weapon;
    // Boolean value if the pickup is allowed
    private bool pickUpAllowed;

    // This method will be called when player presses 'Pick up weapon' keybinding
    public void PlayerWeaponPick()
    {
        /* If weapon is allowed to be picked up (e.g. there is a weapon and player is in the trigger),
        * then pickup the weapon */
        Debug.Log("Trying to pick-up weapon");
        if(pickUpAllowed)
        {
            weapon.GetComponent<WeaponPickup>().PlayerPickupWeapon();
        } else
        {
            Debug.LogWarning("Nothing to pick up!");
        }
    }
    
    // This method will check if it is a weapon when entering a trigger
    protected void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it is a weapon
        if(other.CompareTag("Weapon"))
        {
            // Save the collider of the 'other' as a weapon
            weapon = other;
            // Allow pickup
            pickUpAllowed = true;
        } 
    }
    // When leaving the trigger, if it is a weapon trigger, then forget the weapon
    protected void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Weapon"))
        {
            weapon = null;
            pickUpAllowed = false;
        } 
    }
}
