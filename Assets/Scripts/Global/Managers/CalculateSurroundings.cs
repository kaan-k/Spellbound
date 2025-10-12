using UnityEngine;

public class CalculateSurroundings : MonoBehaviour,ICalculateSurroundings
{
    public Vector2 AngleToVector(float angleDegrees)
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(rad), Mathf.Sin(rad));
    }

    public void CastRayAndMove(int rayCount, Vector3 moveDir,float rayDistance, LayerMask obstacleMask, float avoidStrength, float movementSpeed, Rigidbody2D rigidBody)
    {
        for (int i = 0; i < rayCount; i++)
        {
            float angle = i * (360f / rayCount);
            Vector2 dir = AngleToVector(angle);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, rayDistance, obstacleMask);
            Color rayColor = hit.collider ? Color.red : Color.green;
            Debug.DrawRay(transform.position, dir * rayDistance, rayColor);
            if (hit.collider != null)
            {
                Vector2 avoidDir = Vector2.Reflect(moveDir, hit.normal.normalized);

                moveDir = Vector2.Lerp(moveDir, avoidDir, avoidStrength * Time.deltaTime);
            }
        }
        rigidBody.linearVelocity = moveDir * movementSpeed;
    }
}
