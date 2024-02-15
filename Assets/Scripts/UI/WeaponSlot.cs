using UnityEngine;

// Slot at the bottom right for showing weapon in use
public class WeaponSlot : MonoBehaviour
{

    private PlayerStats playerStats;

    private void Awake()
    {
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        playerStats.OnPlayerWeaponPickup += AddWeaponIcon;
    }

    private void OnDisable()
    {
        playerStats.OnPlayerWeaponPickup -= AddWeaponIcon;
    }


    private void AddWeaponIcon(WeaponPickup pickup)
    {
        RemoveWeaponIcons();
        Instantiate(pickup.iconPrefab, transform);
    }

    private void RemoveWeaponIcons()
    {
        Destroy(gameObject.transform.GetChild(0).gameObject);
    }

}
