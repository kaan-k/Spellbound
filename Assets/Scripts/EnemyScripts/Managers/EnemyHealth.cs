using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public int health = 150;
    public float knockbackForce = 5f;
    public SpriteRenderer theBody;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;

    private Color originalColor;

    private void Start()
    {
        originalColor = theBody.color; 
    }
    private void Update()
    {

    }

  
    public void DamageEnemy(int damage, Vector2 knockbackDirection)
    {
        health -= damage;
        Rigidbody2D rigidBody = this.GetComponent<Rigidbody2D>();
        // Apply knockback force
        rigidBody.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);

        StartCoroutine(FlashEffect());

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashEffect()
    {
        theBody.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        theBody.color = originalColor;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(FlashEffect());

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
