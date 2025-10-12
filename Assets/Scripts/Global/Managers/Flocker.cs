using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flocker : MonoBehaviour, IFlocker
{
    public Transform player;
    public int rayCount;
    public float moveSpeed = 5f;
    public float followDistance = 2f;
    public float separationRadius = 1f;
    public float separationWeight = 1.5f;
    public float rayDistance = 2f;
    public float avoidStrength = 4f;
    public LayerMask obstacleMask;

    //private Rigidbody2D rb;
    private static List<Flocker> allMinions = new List<Flocker>();
    private CalculateSurroundings calculateSurroundings;
    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player").transform;
        //rb = GetComponent<Rigidbody2D>();
        allMinions.Add(this);
    }
    private void Awake()
    {
        calculateSurroundings = GetComponent<CalculateSurroundings>();
    }

    private Vector2 Separation()
    {
        Vector2 separation = Vector2.zero;
        int count = 0;

        foreach (Flocker other in allMinions)
        {
            if (other == this) continue;
            float distance = Vector2.Distance(transform.position, other.transform.position);
            if (distance < separationRadius)
            {
                separation += ((Vector2)(transform.position - other.transform.position)).normalized / distance;
                count++;
            }
        }
        return count > 0 ? separation / count : Vector2.zero;
    }

    public void Flock(Transform player, Rigidbody2D rb)
    {

            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            if (distanceToPlayer > followDistance)
            {
                Vector2 moveDirection = (player.position - transform.position).normalized;
                Vector2 separationForce = Separation();
                Vector2 finalVelocity = moveDirection + separationForce * separationWeight;

                calculateSurroundings.CastRayAndMove(rayCount, finalVelocity,rayDistance, obstacleMask, avoidStrength, moveSpeed, rb);
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }
    

    void OnDestroy()
    {
        allMinions.Remove(this);
    }

}
