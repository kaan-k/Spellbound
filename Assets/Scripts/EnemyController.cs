using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float movementSpeed;
    public int health = 150;
    public float rangeToChase;
    public float separationRadius = 1.5f;
    public float separationForce = 5f;

    private Vector3 moveDir;
    public bool shouldShoot;
    public GameObject projectile;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;
    public float shootingRange;
    public SpriteRenderer theBody;

    public float surroundDistance = 3f; // Preferred distance around the player to surround them
    public float knockbackForce = 5f;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;
    private Color originalColor;

    private void Start()
    {
        originalColor = theBody.color; // Store the original color for flash effect
    }

    private void Update()
    {
        if (theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy)
        {
            Vector3 targetPosition = CalculateSurroundPosition();
            moveDir = (PlayerController.instance.transform.position - transform.position).normalized;

            // Convert separation to Vector3 to match moveDir type
            rigidBody.velocity = (moveDir * movementSpeed) + (Vector3)CalculateSeparation();

            // Shooting logic
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

    private Vector2 CalculateSeparation()
    {
        Vector2 separation = Vector2.zero;
        var nearbyEnemies = FindObjectsOfType<EnemyController>();

        foreach (var enemy in nearbyEnemies)
        {
            if (enemy != this)
            {
                float distance = Vector2.Distance(transform.position, enemy.transform.position);

                if (distance < separationRadius)
                {
                    Vector2 directionAway = (transform.position - enemy.transform.position).normalized;
                    separation += directionAway * (separationForce / distance);
                }
            }
        }

        return separation;
    }

    private Vector3 CalculateSurroundPosition()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;
        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;

        float angleOffset = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        angleOffset += Random.Range(-30, 30); // Add some randomness to make the movement less predictable

        float radianAngle = angleOffset * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0) * surroundDistance;

        return playerPosition + offset;
    }

    public void DamageEnemy(int damage, Vector2 knockbackDirection)
    {
        health -= damage;

        // Apply knockback force
        rigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        // Apply flash effect
        StartCoroutine(FlashEffect());

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Flash effect coroutine
    private IEnumerator FlashEffect()
    {
        theBody.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        theBody.color = originalColor;
    }
}
