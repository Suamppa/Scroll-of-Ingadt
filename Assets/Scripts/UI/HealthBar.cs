using System.Collections;
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
    // Reference to the shield icon
    public GameObject shieldIcon;

    // Player's Stats component
    private PlayerStats playerStats;
    private TMP_Text shieldAmountText;
    private TMP_Text shieldDurationText;
    private IEnumerator shieldCountdown;
    private float shieldDuration = 0f;

    private void Awake() {
        // Find the player's Stats component
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        TMP_Text[] shieldTexts = shieldIcon.GetComponentsInChildren<TMP_Text>();
        shieldAmountText = shieldTexts[0];
        shieldDurationText = shieldTexts[1];
        shieldIcon.SetActive(false);
    }

    private void OnEnable() {
        // Subscribe to the OnPlayerDamaged event
        playerStats.OnPlayerDamaged += UpdateHealthIndicators;
        playerStats.OnPlayerHealed += UpdateHealthIndicators;
        TempShield.OnTempShieldApplied += UpdateShieldIndicator;
        TempShield.OnTempShieldRemoved += UpdateShieldIndicator;
    }

    private void OnDisable() {
        // Unsubscribe from the OnPlayerDamaged event
        playerStats.OnPlayerDamaged -= UpdateHealthIndicators;
        playerStats.OnPlayerHealed -= UpdateHealthIndicators;
        TempShield.OnTempShieldApplied -= UpdateShieldIndicator;
        TempShield.OnTempShieldRemoved -= UpdateShieldIndicator;
    }

    private void Start() {
        // Get the player's current health
        int health = playerStats.maxHealth;
        // Set the health indicators to the player's current health
        UpdateHealthIndicators(health);
    }

    // Update the health indicators to the player's current health
    public void UpdateHealthIndicators(int health) {
        // If the player is dead, set all health indicators to empty
        // While not strictly necessary, it's a speedup avoiding unnecessary loops
        if (health < 1) {
            foreach (Image heart in hearts) {
                heart.sprite = emptyHeart;
            }
            return;
        }
        // Loop through the health indicators up to health
        for (int i = 0; i < health; i++) {
            if (i % 2 == 1) {
                // If the index is odd, set the sprite to fullHeart
                hearts[i/2].sprite = fullHeart;
            } else {
                // If the index is even, set the sprite to halfHeart
                hearts[i/2].sprite = halfHeart;
            }
        }
        // Loop through the remaining health indicators
        for (int i = (health+1)/2; i < hearts.Length; i++) {
            hearts[i].sprite = emptyHeart;
        }
    }

    public void UpdateShieldIndicator(float duration, int shieldAmount) {
        if (playerStats.shield > 0)
        {
            // Update shield amount indicator
            if (!shieldIcon.activeSelf) shieldIcon.SetActive(true);
            shieldAmountText.text = shieldAmount.ToString();

            // Update or add countdown timer
            if (shieldCountdown != null)
            {
                StopCoroutine(shieldCountdown);
            }
            shieldCountdown = ShieldCountdown(duration);
            StartCoroutine(shieldCountdown);
        }
        else
        {
            if (shieldIcon.activeSelf) shieldIcon.SetActive(false);
        }
    }

    // TODO: Create Timer class
    private IEnumerator ShieldCountdown(float duration) {
        shieldDuration += duration;
        while (shieldDuration > 0)
        {
            shieldDurationText.text = shieldDuration.ToString("F1");
            yield return new WaitForSeconds(0.1f);
            shieldDuration -= 0.1f;
        }
        shieldIcon.SetActive(false);
    }
}
