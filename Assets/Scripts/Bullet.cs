using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 7.0f;
    public Rigidbody2D rb;

    public int dmg = 50;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "projectile")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Enemy")
        {
            // Calculate the knockback direction (from bullet to enemy)
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;

            // Apply damage and knockback to the enemy
            collision.GetComponent<EnemyController>().DamageEnemy(dmg, knockbackDirection);

            // Destroy the bullet after it hits the enemy
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
