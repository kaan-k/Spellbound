using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour,IDamageable
{
    public int health;
    public SpriteRenderer theBody;
    public Color flashColor = Color.red;
    public float flashDuration = 0.1f;
    private Color originalColor;


    void Start()
    {
        originalColor = theBody.color;
    }
    public void TakeDamage(int damage)
    {
        if (health < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            health = health - damage;
            StartCoroutine(FlashEffect());
        }
    }

    private IEnumerator FlashEffect()
    {
        theBody.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        theBody.color = originalColor;
    }

}
