using TMPro;
using UnityEngine;

// Slot at the bottom right for showing weapon in use
public class WeaponSlot : MonoBehaviour
{
    private GameObject activeIcon;
    private PlayerStats playerStats;

    private void OnEnable()
    {
        playerStats = GameManager.Instance.PlayerInstance.GetComponent<PlayerStats>();
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
        string text = $"Damage: {playerStats.Damage}\nSpeed: {playerStats.AttackSpeedSeconds}/s";
        // activeIcon.GetComponentInChildren<TextMeshPro>().SetText(text);
        activeIcon.GetComponentInChildren<TMP_Text>().text = text;

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
