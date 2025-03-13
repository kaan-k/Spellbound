using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour, IDistanceCalculator
{
    public float CalculateDistance(Transform from, Transform to)
    {
        return Vector2.Distance(from.position, to.position);
    }
}
