using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth;
    public int maxHealth;

    public float invisLength = 1f;
    private float invisCount;

    public void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;

        // Check if UIController exists and if the UI elements are assigned
        if (UIController.instance != null)
        {
            if (UIController.instance.healthSlider != null)
            {
                UIController.instance.healthSlider.maxValue = maxHealth;
                UIController.instance.healthSlider.value = currentHealth;
            }
            if (UIController.instance.healthText != null)
            {
                UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
            }
        }
        else
        {
            Debug.LogError("UIController instance is missing.");
        }
    }

    void Update()
    {
        if (invisCount > 0)
        {
            invisCount -= Time.deltaTime;
            if (invisCount <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);
            }
        }
    }

    public void DamagePlayer()
    {
        // Separate function for invincibility frames
        if (invisCount <= 0)
        {
            currentHealth--;
            invisCount = invisLength;
            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);

            if (currentHealth <= 0)
            {
                if (UIController.instance != null && UIController.instance.deathScreen != null)
                {
                    UIController.instance.deathScreen.SetActive(true);
                }
                else
                {
                    Debug.LogError("Death screen is not set in UIController.");
                }
            }

            if (UIController.instance != null)
            {
                if (UIController.instance.healthSlider != null)
                {
                    UIController.instance.healthSlider.value = currentHealth;
                }
                if (UIController.instance.healthText != null)
                {
                    UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
                }
            }
        }
    }

    public void InvincibilityFrames()
    {
        if (invisCount <= 0)
        {
            invisCount = invisLenght;
            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        // Update the UI after healing
        if (UIController.instance != null)
        {
            if (UIController.instance.healthSlider != null)
            {
                UIController.instance.healthSlider.value = currentHealth;
            }
            if (UIController.instance.healthText != null)
            {
                UIController.instance.healthText.text = currentHealth + " / " + maxHealth;
            }
        }
    }
}
