using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour, IPlayerMovement
{
    [Header("Movement Settings")]
    public float movementSpeed;
    public Rigidbody2D rigidBody;
    public SpriteRenderer bodySR;


    private float speedSmoothing = 0.1f;
    private float smoothedSpeed;


    private float activeMoveSpeed;

    private Vector2 moveInput;

    private void Start()
    {
        activeMoveSpeed = movementSpeed;
    }

    private void Update()
    {
        UpdateMovement();
    }

    public void UpdateMovement()
    {

        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();


        rigidBody.velocity = moveInput * activeMoveSpeed;


        float targetSpeed = moveInput.magnitude;
        smoothedSpeed = Mathf.Lerp(smoothedSpeed, targetSpeed, speedSmoothing);


        if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public float CurrentSpeed => smoothedSpeed;

    public void SetMoveSpeed(float newSpeed)
    {
        activeMoveSpeed = newSpeed;
    }
}
