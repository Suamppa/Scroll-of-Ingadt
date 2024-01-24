using UnityEngine;
using UnityEngine.UI;

// Replaced by HealthBar.cs
public class HUDLogic : MonoBehaviour
{
    // Reference to the health indicators' Image components
    public Image[] hearts;
    // References to the sprites for the health indicators
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;
    // Reference to the shield icon
    public Sprite shieldIcon;

    // Player's Stats component
    private Stats playerStats;

    private void Awake() {
        // Find the player's Stats component
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Stats>();
    }

    private void OnEnable() {
        // Subscribe to the OnPlayerDamaged event
        PlayerStats.OnPlayerDamaged += UpdateHealthIndicators;
        PlayerStats.OnPlayerHealed += UpdateHealthIndicators;
    }

    private void OnDisable() {
        // Unsubscribe from the OnPlayerDamaged event
        PlayerStats.OnPlayerDamaged -= UpdateHealthIndicators;
        PlayerStats.OnPlayerHealed -= UpdateHealthIndicators;
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
}
