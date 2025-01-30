using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMinion : MonoBehaviour
{
    Vector3 moveDir;
    public Rigidbody2D rigidBody;
    public float movementSpeed;
    public GameObject selectedEnemy;
    // Start is called before the first frame update
    void Start()
    {
        Quaternion target = Quaternion.Euler(0, 0, 0);
        this.transform.rotation = target;
        selectedEnemy = GameObject.FindWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedEnemy != null)
        {
            moveDir = (selectedEnemy.transform.position - transform.position).normalized;
            rigidBody.velocity = (moveDir * movementSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyController>().DamageEnemy(10,Vector2.right);
            Destroy(this.gameObject);
        }
    }
}
