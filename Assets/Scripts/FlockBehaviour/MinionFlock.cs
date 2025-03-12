using System.Collections.Generic;
using UnityEngine;

public class MinionFlock : MonoBehaviour
{
    [Header("Flocking Settings")]
    public float moveSpeed = 5f;
    public float followDistance = 2f;
    public float separationRadius = 1f;
    public float separationWeight = 1.5f;

    private Rigidbody2D rb;
    private FlockingMovement flockingMovement;
    public FireMinion fireMinion; 

    private static List<MinionFlock> allMinions = new List<MinionFlock>();

    private Transform player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        fireMinion = GetComponent<FireMinion>();


        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }

        flockingMovement = new FlockingMovement(
            new FollowBehavior(),
            new SeparationBehavior(),
            player,
            followDistance,
            moveSpeed,
            separationWeight,
            separationRadius
        );
    }

    private void Start()
    {
        allMinions.Add(this);
    }

    private void FixedUpdate()
    {
        // Only execute flocking behavior if this minion does not have an enemy target.
        if (fireMinion != null && !fireMinion.hasTarget)
        {
            // Build a list of neighbor transforms.
            List<Transform> neighbors = new List<Transform>();
            foreach (MinionFlock minion in allMinions)
            {
                if (minion != this)
                {
                    neighbors.Add(minion.transform);
                }
            }

            // Calculate the velocity based on flocking behavior.
            Vector2 velocity = flockingMovement.CalculateVelocity(transform, neighbors);
            rb.velocity = velocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnDestroy()
    {
        allMinions.Remove(this);
    }
}
