using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public int currentHealth;
    public int maxHealth;


    public float invisLenght = 1f;
    private float invisCount;
    public void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
        UIController.instance.healthSlider.maxValue = maxHealth;
        UIController.instance.healthSlider.value = currentHealth;
        UIController.instance.healthText.text = currentHealth + " " + "/" + " " + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(invisCount > 0)
        {
            invisCount -= Time.deltaTime;
            if(invisCount <= 0)
            {
                PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, 1f);
            }
        }
    }

    public void DamagePlayer()
    {

        // Seperate function for invincibility frames.

        if (invisCount <= 0)
        {
            currentHealth--;
            invisCount = invisLenght;
            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b,.5f);
            if (currentHealth <= 0)
            {
                UIController.instance.deathScreen.SetActive(true);
            }
            UIController.instance.healthSlider.value = currentHealth;
            UIController.instance.healthText.text = currentHealth + " " + "/" + " " + maxHealth;
        }

    }

    public void InvinciblityFrames()
    {
        if(invisCount <= 0)
        {
            invisCount = invisLenght;
            PlayerController.instance.bodySR.color = new Color(PlayerController.instance.bodySR.color.r, PlayerController.instance.bodySR.color.g, PlayerController.instance.bodySR.color.b, .5f);
        }
    }
    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;
    }
}
