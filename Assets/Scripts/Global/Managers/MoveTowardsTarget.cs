using UnityEngine;

public class MoveTowardsTarget : MonoBehaviour, IMovable
{
    private Rigidbody2D rigidBody;
    public float movementSpeed;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Move(GameObject target)
    {
        if (target == null) return;

        Vector3 moveDir = (target.transform.position - transform.position).normalized;
        rigidBody.velocity = moveDir * movementSpeed;
    }
}