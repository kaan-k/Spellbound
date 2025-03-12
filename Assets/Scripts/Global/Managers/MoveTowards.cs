using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowards : MonoBehaviour, IMovable
{
    private Rigidbody2D rigidBody;
    public float movementSpeed;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(GameObject selectedEnemy)
    {
        if (selectedEnemy == null) return;

        Vector3 moveDir = (selectedEnemy.transform.position - transform.position).normalized;
        rigidBody.velocity = moveDir * movementSpeed;
    }
}
