using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Transform healthBar; // Reference to the 2D health bar object
    public Vector3 healthBarOffset = new Vector3(0, 1, 0); // Position offset for the health bar

    private Vector3 originalScale; // Store the original scale of the health bar

    void Start()
    {
        // Initialize health
        currentHealth = maxHealth;

        // Store the original scale of the health bar
        if (healthBar != null)
        {
            originalScale = healthBar.localScale;
        }
    }

    void Update()
    {
        // Ensure the health bar stays under the player and does not rotate
        if (healthBar != null)
        {
            healthBar.position = transform.position + healthBarOffset;

            // Reset rotation to keep it static
            healthBar.rotation = Quaternion.identity;
        }
    }

    public void TakeDamage(float damage)
    {
        // Reduce the player's health
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar's scale
        if (healthBar != null)
        {
            float healthPercent = currentHealth / maxHealth;
            healthBar.localScale = new Vector3(originalScale.x * healthPercent, originalScale.y, originalScale.z);
        }
    }

    public void Heal(float amount)
    {
        // Heal the player's health
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update the health bar's scale
        if (healthBar != null)
        {
            float healthPercent = currentHealth / maxHealth;
            healthBar.localScale = new Vector3(originalScale.x * healthPercent, originalScale.y, originalScale.z);
        }
    }
}
