using UnityEngine;

public class FollowBehavior : IFollowBehavior
{
    public Vector2 GetFollowDirection(Transform agentTransform, Transform target, float followDistance)
    {
        float distance = Vector2.Distance(agentTransform.position, target.position);
        if (distance > followDistance)
        {
            return (target.position - agentTransform.position).normalized;
        }
        return Vector2.zero;
    }
}
