using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float movementSpeed;
    public int health = 150;

    public float rangeToChase;
    private Vector3 moveDir;

    public bool shouldShoot;

    public GameObject projectile;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;

    public float shootingRange;

    public SpriteRenderer theBody;

    private FlashEffect flashEffect; // Reference to the FlashEffect script

    // Knockback settings
    public float knockbackForce = 5f; // Adjust this value to control the strength of the knockback
    public float knockbackDuration = 0.2f; // Duration of the knockback effect
    private bool isKnockedBack = false; // To track if the enemy is currently knocked back
    private float knockbackCounter;

    void Start()
    {
        flashEffect = GetComponent<FlashEffect>(); // Get the FlashEffect component
        if (flashEffect == null)
        {
            Debug.LogError("FlashEffect component is missing on the Enemy!");
        }
    }

    void Update()
    {
        if (isKnockedBack)
        {
            knockbackCounter -= Time.deltaTime;
            if (knockbackCounter <= 0)
            {
                isKnockedBack = false; // Reset knockback state
                rigidBody.velocity = Vector2.zero; // Stop the knockback force
            }
            return; // Skip the regular movement and shooting logic while knocked back
        }

        if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChase)
            {
                moveDir = PlayerController.instance.transform.position - transform.position;
            }
            else
            {
                moveDir = Vector3.zero;
            }
            moveDir.Normalize();

            rigidBody.velocity = moveDir * movementSpeed;

            if (shouldShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootingRange)
            {
                fireCounter -= Time.deltaTime;

                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(projectile, firePoint.position, firePoint.rotation);
                }
            }
        }
        else
        {
            rigidBody.velocity = Vector3.zero;
        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;

        // Trigger flash effect when the enemy takes damage
        if (flashEffect != null)
        {
            flashEffect.TriggerFlash();
        }

        ApplyKnockback();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Method to apply knockback effect
    private void ApplyKnockback()
    {
        // Calculate direction for knockback
        Vector2 knockbackDirection = (transform.position - PlayerController.instance.transform.position).normalized;

        // Apply force to the Rigidbody2D
        rigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        // Set knockback state
        isKnockedBack = true;
        knockbackCounter = knockbackDuration;
    }
}
