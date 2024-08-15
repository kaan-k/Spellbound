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
        else
        {

        }

        if(collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyController>().DamageEnemy(dmg);
        }


    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
