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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (!collision.gameObject.CompareTag("projectile"))
        //{
        //    Destroy(gameObject);
        //}

        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Calculate the knockback direction (from bullet to enemy)
            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;

            // Apply damage and knockback to the enemy
            //collision.gameObject.GetComponent<EnemyController>().DamageEnemy(dmg, knockbackDirection);

            collision.gameObject.GetComponent<Damageable>().TakeDamage(dmg);
            // Destroy the bullet after it hits the enemy
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
