using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDLogic : MonoBehaviour
{
    // Reference to the health indicators' Image components
    public Image[] hearts;
    // References to the sprites for the health indicators
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    // Player's Stats component
    private Stats playerStats;

    private void Awake() {
        // Find the player's Stats component
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }

    private void OnEnable() {
        // Subscribe to the OnPlayerDamaged event
        PlayerStats.OnPlayerDamaged += UpdateHealthIndicators;
    }

    private void OnDisable() {
        // Unsubscribe from the OnPlayerDamaged event
        PlayerStats.OnPlayerDamaged -= UpdateHealthIndicators;
    }

    private void Start() {
        // Get the player's current health
        int health = playerStats.currentHealth;
        // Set the health indicators to the player's current health
        UpdateHealthIndicators(health);
    }

    // Update the health indicators to the player's current health
    public void UpdateHealthIndicators(int health) {
        // Set the health indicators accordingly
        for (int i = 0; i < health; i += 2) {
            // Even health values are always full hearts
            hearts[i/2].sprite = fullHeart;
        }
        if (health % 2 == 1) {
            // If current health is odd, the last heart is a half heart
            hearts[health/2].sprite = halfHeart;
        }
        for (int i = health/2 + 1; i < hearts.Length; i++) {
            // The rest of the hearts are empty
            hearts[i].sprite = emptyHeart;
        }
    }
}
