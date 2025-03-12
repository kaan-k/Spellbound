using UnityEngine;

public interface IMover
{
    void MoveTowards(Vector3 targetPosition, Vector3 currentPosition);
}
