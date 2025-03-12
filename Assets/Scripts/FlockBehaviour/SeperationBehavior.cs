using System.Collections.Generic;
using UnityEngine;

public class SeparationBehavior : ISeparationBehavior
{
    public Vector2 GetSeparationForce(Transform agentTransform, IEnumerable<Transform> neighbors, float separationRadius)
    {
        Vector2 separation = Vector2.zero;
        int count = 0;
        foreach (Transform neighbor in neighbors)
        {
            // Skip self.
            if (neighbor == agentTransform)
                continue;

            float distance = Vector2.Distance(agentTransform.position, neighbor.position);
            if (distance < separationRadius && distance > 0f)
            {
                separation += ((Vector2)(agentTransform.position - neighbor.position)).normalized / distance;
                count++;
            }
        }
        return count > 0 ? separation / count : Vector2.zero;
    }
}
