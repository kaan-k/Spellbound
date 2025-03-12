using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberryEnemy : MonoBehaviour
{
    private EnemyMovement enemyMovement;


    private void Awake()
    {
        enemyMovement = GetComponent<EnemyMovement>();
    }

    private void Update()
    {
        enemyMovement.Move();
    }
}
