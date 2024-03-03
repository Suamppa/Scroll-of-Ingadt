using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Bar at the top right of the screen that shows icons for status effects if they occur
public class StatusBar : MonoBehaviour
{

    private PlayerStats playerStats;
    private List<(GameObject Icon, Timer Countdown)> activeStatuses;

    private void Awake()
    {
        playerStats = GameManager.Instance.PlayerInstance.GetComponent<PlayerStats>();
        activeStatuses = new List<(GameObject Icon, Timer Countdown)>();
    }

    private void OnEnable()
    {
        playerStats.OnPlayerStatus += AddStatusIcon;
    }

    private void OnDisable()
    {
        playerStats.OnPlayerStatus -= AddStatusIcon;
    }

    // TODO: Add text for boost amount on top of the icon
    private void AddStatusIcon(TemporaryPickup pickup)
    {
        GameObject icon = Instantiate(pickup.iconPrefab, transform);
        pickup.Timer.AssignText(icon.GetComponentInChildren<TMP_Text>());
        activeStatuses.Add((icon, pickup.Timer));
        pickup.Timer.OnTimerEnd += () => RemoveStatusIcon(icon);
    }


    private void RemoveStatusIcon(GameObject icon)
    {
        int index = activeStatuses.FindIndex(status => status.Icon == icon);
        if (index >= 0 && index < activeStatuses.Count)
        {
            Destroy(activeStatuses[index].Icon);
            activeStatuses.RemoveAt(index);
        }
    }
}
