using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    // Reference to the health indicators' Image components
    public Image[] hearts;
    // References to the sprites for the health indicators
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    // Player's Stats component
    private PlayerStats playerStats;

    private void Awake()
    {
        // Find the player's Stats component
        playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats>();
    }

    private void OnEnable()
    {
        // Subscribe to the OnPlayerDamaged event
        playerStats.OnPlayerDamaged += UpdateHealthIndicators;
        playerStats.OnPlayerHealed += UpdateHealthIndicators;
        playerStats.OnPlayerTempShield += AddTempShieldIcon;
    }

    private void OnDisable()
    {
        // Unsubscribe from the OnPlayerDamaged event
        playerStats.OnPlayerDamaged -= UpdateHealthIndicators;
        playerStats.OnPlayerHealed -= UpdateHealthIndicators;
        playerStats.OnPlayerTempShield -= AddTempShieldIcon;
    }

    private void Start()
    {
        // Get the player's current health
        int health = playerStats.MaxHealth;
        // Set the health indicators to the player's current health
        UpdateHealthIndicators(health);
    }

    // Update the health indicators to the player's current health
    public void UpdateHealthIndicators(int health)
    {
        // If the player is dead, set all health indicators to empty
        // While not strictly necessary, it's a speedup avoiding unnecessary loops
        if (health < 1)
        {
            foreach (Image heart in hearts)
            {
                heart.sprite = emptyHeart;
            }
            return;
        }
        // Loop through the health indicators up to health
        for (int i = 0; i < health; i++)
        {
            if (i % 2 == 1)
            {
                // If the index is odd, set the sprite to fullHeart
                hearts[i / 2].sprite = fullHeart;
            }
            else
            {
                // If the index is even, set the sprite to halfHeart
                hearts[i / 2].sprite = halfHeart;
            }
        }
        // Loop through the remaining health indicators
        for (int i = (health + 1) / 2; i < hearts.Length; i++)
        {
            hearts[i].sprite = emptyHeart;
        }
    }

    private void AddTempShieldIcon(TempShield shield)
    {
        GameObject icon = Instantiate(shield.iconPrefab, transform);
        TMP_Text[] shieldTexts = icon.GetComponentsInChildren<TMP_Text>();
        TMP_Text shieldAmountText = shieldTexts[0];
        TMP_Text shieldTimerText = shieldTexts[1];

        shieldAmountText.text = shield.shieldAmount.ToString();
        shield.Timer.AssignText(shieldTimerText);
        shield.Timer.OnTimerEnd += () => RemoveTempShieldIcon(icon);
        playerStats.OnPlayerShieldLost += (shieldValue) => UpdateTempShieldIcon(shieldAmountText, shieldValue, shield.Timer);
    }

    private void UpdateTempShieldIcon(TMP_Text shieldAmountText, int shieldAmount, Timer timer)
    {
        shieldAmountText.text = shieldAmount.ToString();
        if (shieldAmount < 1 && timer.IsRunning)
        {
            // This will trigger the OnTimerEnd event
            timer.StopTimer();
        }
    }

    private void RemoveTempShieldIcon(GameObject icon)
    {
        Destroy(icon);
    }
}
