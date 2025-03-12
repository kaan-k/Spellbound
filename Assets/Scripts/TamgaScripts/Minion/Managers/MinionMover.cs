using UnityEngine;

public class MinionMover : IMover
{
    private Rigidbody2D rigidBody;
    private float movementSpeed;

    public MinionMover(Rigidbody2D rigidBody, float movementSpeed)
    {
        this.rigidBody = rigidBody;
        this.movementSpeed = movementSpeed;
    }

    public void MoveTowards(Vector3 targetPosition, Vector3 currentPosition)
    {
        Vector3 direction = (targetPosition - currentPosition).normalized;
        rigidBody.velocity = direction * movementSpeed;
    }
}
