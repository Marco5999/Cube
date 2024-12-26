using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public SpriteRenderer spriteRenderer;  // Reference to the sprite renderer for flashing effect
    public Color flashColor = Color.grey;  // Color to flash when damaged
    private Color originalColor;
    private bool isFlashing = false;

    public float flashDuration = 0.1f;  // How long the flash lasts

    void Start()
    {
        // Initialize health and the sprite renderer
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void Update()
    {
        if (isFlashing)
        {
            // If flashing, revert to the original color after a brief duration
            flashDuration -= Time.deltaTime;
            if (flashDuration <= 0)
            {
                spriteRenderer.color = originalColor;
                isFlashing = false;
                flashDuration = 0.1f;  // Reset the duration
            }
        }
    }

    // Method to apply damage to the enemy
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);  // Ensure health doesn't go below 0

        // Start flashing effect
        if (!isFlashing)
        {
            StartCoroutine(Flash());
        }

        // Optionally: You can destroy the enemy if health reaches 0
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Flash the enemy color to indicate damage
    private IEnumerator Flash()
    {
        isFlashing = true;
        spriteRenderer.color = flashColor;
        yield return new WaitForSeconds(0.1f);  // Duration of the flash
        spriteRenderer.color = originalColor;
        isFlashing = false;
    }
}
