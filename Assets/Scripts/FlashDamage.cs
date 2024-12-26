using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashDamage : MonoBehaviour
{
    public Color flashColor = Color.gray; // Color to flash
    public float flashDuration = 0.1f; // How long the flash lasts

    private SpriteRenderer[] spriteRenderers; // Array to hold all SpriteRenderers
    private Color[] originalColors; // Array to store original colors

    void Start()
    {
        // Get all SpriteRenderers in the player and its children
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        // Store original colors of all SpriteRenderers
        originalColors = new Color[spriteRenderers.Length];
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            originalColors[i] = spriteRenderers[i].color;
        }
    }

    public void Flash()
    {
        // Start the flash coroutine
        StartCoroutine(FlashRoutine());
    }

    private System.Collections.IEnumerator FlashRoutine()
    {
        // Change the color of all SpriteRenderers to the flash color
        foreach (var sr in spriteRenderers)
        {
            sr.color = flashColor;
        }

        // Wait for the flash duration
        yield return new WaitForSeconds(flashDuration);

        // Revert all SpriteRenderers back to their original colors
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            spriteRenderers[i].color = originalColors[i];
        }
    }
}
