using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour, IMoveable
{
    public Rigidbody2D rigidBody;
    public float movementSpeed;
    public float separationRadius = 1.5f;
    public float separationForce = 5f;
    public float surroundDistance = 3f;

    public SpriteRenderer theBody;

    private void Update()
    {
        Move();
    }

    private bool IsActiveAndVisible()
    {
        return theBody.isVisible && PlayerController.instance.gameObject.activeInHierarchy;
    }

    private Vector2 CalculateSeparation()
    {
        Vector2 separation = Vector2.zero;
        EnemyMovement[] nearbyEnemies = FindObjectsOfType<EnemyMovement>();

        foreach (EnemyMovement enemy in nearbyEnemies)
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


    public Vector3 CalculateSurroundPosition()
    {
        Vector3 playerPosition = PlayerController.instance.transform.position;
        Vector3 directionToPlayer = (transform.position - playerPosition).normalized;
        float angleOffset = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        angleOffset += Random.Range(-30f, 30f); 
        float radianAngle = angleOffset * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(radianAngle), Mathf.Sin(radianAngle), 0) * surroundDistance;
        return playerPosition + offset;
    }

    public void Move()
    {
        if (IsActiveAndVisible())
        {

            Vector3 moveDir = (PlayerController.instance.transform.position - transform.position).normalized;

            Vector2 separation = CalculateSeparation();
            rigidBody.velocity = (moveDir * movementSpeed) + (Vector3)separation;
        }
        else
        {
            rigidBody.velocity = Vector3.zero;
        }
    }
}
