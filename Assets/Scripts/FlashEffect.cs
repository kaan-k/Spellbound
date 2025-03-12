using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    public Color flashColor = Color.white; // The color to flash to
    public float flashDuration = 0.1f; // Duration of the flash

    void Start()
    {
        // Try to get the SpriteRenderer on this GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // If not found, try to get it on the children
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        // Check again if we have a SpriteRenderer
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color; // Store the original color of the sprite
        }
        else
        {
            Debug.LogError("No SpriteRenderer found on this GameObject or its children!");
        }
    }

    // Method to trigger the flash effect
    public void TriggerFlash()
    {
        if (spriteRenderer != null)
        {
            StartCoroutine(FlashCoroutine());
        }
    }

    // Coroutine to handle the flashing logic
    private IEnumerator FlashCoroutine()
    {
        spriteRenderer.color = flashColor; // Change to flash color
        yield return new WaitForSeconds(flashDuration); // Wait for the flash duration
        spriteRenderer.color = originalColor; // Revert to the original color
    }
}
