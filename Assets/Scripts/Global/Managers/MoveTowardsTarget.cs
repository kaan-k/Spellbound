using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour, IMovable
{
    private Rigidbody2D rigidBody;
    private CalculateSurroundings calculateSurroundings;
    public float movementSpeed;
    public int rayCount;


    public float rayDistance = 2f;
    public float avoidStrength = 4f;
    public LayerMask obstacleMask;
    private void Awake()
    {
        calculateSurroundings = GetComponent<CalculateSurroundings>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(GameObject target)
    {
        if (target == null) return;

        Vector3 moveDir = (target.transform.position - transform.position).normalized;
        calculateSurroundings.CastRayAndMove(rayCount, moveDir, rayDistance, obstacleMask, avoidStrength, movementSpeed, rigidBody);
    } 
}