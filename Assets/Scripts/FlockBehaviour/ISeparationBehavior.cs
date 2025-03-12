using System.Collections.Generic;
using UnityEngine;

public interface ISeparationBehavior
{
    Vector2 GetSeparationForce(Transform agentTransform, IEnumerable<Transform> neighbors, float separationRadius);
}
