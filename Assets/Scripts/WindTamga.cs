using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTamga : Bullet
{
    public float knockbackForce = 5f;
    public Vector2 windDirection = new Vector2(1, 0); // Default: Rightward wind

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Reset velocity to prevent stacking force
            rb.AddForce(windDirection.normalized * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
