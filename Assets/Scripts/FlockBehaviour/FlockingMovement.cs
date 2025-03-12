using System.Collections.Generic;
using UnityEngine;

public class FlockingMovement
{
    private readonly IFollowBehavior followBehavior;
    private readonly ISeparationBehavior separationBehavior;
    private readonly Transform target;
    private readonly float followDistance;
    private readonly float moveSpeed;
    private readonly float separationWeight;
    private readonly float separationRadius;

    public FlockingMovement(IFollowBehavior followBehavior, ISeparationBehavior separationBehavior,
                            Transform target, float followDistance, float moveSpeed,
                            float separationWeight, float separationRadius)
    {
        this.followBehavior = followBehavior;
        this.separationBehavior = separationBehavior;
        this.target = target;
        this.followDistance = followDistance;
        this.moveSpeed = moveSpeed;
        this.separationWeight = separationWeight;
        this.separationRadius = separationRadius;
    }


    public Vector2 CalculateVelocity(Transform agentTransform, IEnumerable<Transform> neighbors)
    {
        Vector2 followDir = followBehavior.GetFollowDirection(agentTransform, target, followDistance);
        Vector2 separationForce = separationBehavior.GetSeparationForce(agentTransform, neighbors, separationRadius);
        Vector2 finalVelocity = (followDir + separationForce * separationWeight).normalized * moveSpeed;
        return finalVelocity;
    }
}
