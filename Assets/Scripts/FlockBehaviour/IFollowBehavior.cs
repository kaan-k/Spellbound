using UnityEngine;

public interface IFollowBehavior
{
    Vector2 GetFollowDirection(Transform agentTransform, Transform target, float followDistance);
}
