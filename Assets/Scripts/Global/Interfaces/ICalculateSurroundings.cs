using UnityEngine;

public interface ICalculateSurroundings
{
    void CastRayAndMove(int rayCount, Vector3 moveDir, float rayDistance, LayerMask obstacleMask, float avoidStrength, float movementSpeed, Rigidbody2D rigidBody);
    Vector2 AngleToVector(float angleDegrees);
}
