using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFader : MonoBehaviour
{
    public CanvasGroup startTextCanvasGroup; // Reference to the CanvasGroup component on the text
    public float fadeDuration = 1f; // Duration of each fade-in and fade-out cycle
    private bool isFading = false; // Flag to track if fading has started

    void Start()
    {
        // Start the blinking coroutine
        StartCoroutine(BlinkText());
    }

    void Update()
    {
        // Check if any key is pressed and stop blinking
        if (Input.anyKeyDown && !isFading)
        {
            // Stop all coroutines and start fading out
            StopAllCoroutines();
            StartCoroutine(FadeOutText());
        }
    }

    // Coroutine to blink text by fading in and out
    System.Collections.IEnumerator BlinkText()
    {
        while (true)
        {
            // Fade in
            yield return StartCoroutine(Fade(0, 1, fadeDuration));
            // Fade out
            yield return StartCoroutine(Fade(1, 0, fadeDuration));
        }
    }

    // Coroutine to fade text between two alpha values
    System.Collections.IEnumerator Fade(float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            startTextCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }
        startTextCanvasGroup.alpha = endAlpha;
    }

    // Function to fade out the text completely
    System.Collections.IEnumerator FadeOutText()
    {
        isFading = true; // Set the fading flag to true

        float startAlpha = startTextCanvasGroup.alpha; // Store the initial alpha
        float elapsedTime = 0f;

        // Gradually fade out the text
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            startTextCanvasGroup.alpha = Mathf.Lerp(startAlpha, 0, elapsedTime / fadeDuration);
            yield return null;
        }

        // Ensure alpha is set to 0 (fully transparent)
        startTextCanvasGroup.alpha = 0;
        isFading = false;
    }
}
