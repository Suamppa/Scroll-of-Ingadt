using TMPro;
using UnityEngine;

// Slot at the bottom right for showing weapon in use
public class WeaponSlot : MonoBehaviour
{
    private GameObject activeIcon;
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
        RemoveWeaponIcon();
        activeIcon = Instantiate(pickup.iconPrefab, transform);
        string text = $"Damage: {pickup.WeaponDamage}\nSpeed: {pickup.AttackSpeedSeconds}/s";
        // activeIcon.GetComponentInChildren<TextMeshPro>().SetText(text);
        activeIcon.GetComponentInChildren<TMP_Text>().text = $"Damage: {pickup.WeaponDamage}\nSpeed: {pickup.AttackSpeedSeconds}/s";

        if (Debug.isDebugBuild)
        {
            Debug.Log($"Weapon slot text updated to {text}");
        }
    }

    private void RemoveWeaponIcon()
    {
        if (activeIcon != null) Destroy(activeIcon);
    }
}
