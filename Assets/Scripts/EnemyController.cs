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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
