using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    public float scrollSpeed = 0.2f; // Speed of the scrolling background
    public RectTransform backgroundImage; // Reference to the background image's RectTransform

    private Vector2 startPos;

    void Start()
    {
        // Store the starting position of the background image
        startPos = backgroundImage.anchoredPosition;
    }

    void Update()
    {
        // Calculate the new position of the background image
        float newPosX = Mathf.Repeat(Time.time * scrollSpeed, backgroundImage.rect.width);
        backgroundImage.anchoredPosition = startPos + new Vector2(newPosX, 0);
    }
}
