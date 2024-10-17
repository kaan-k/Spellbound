using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float movementSpeed;

    public Rigidbody2D rigidBody;

    public Animator animator;

    public GameObject firingObject;
    public Transform firePoint;

    public float timeBetweenCasts;
    private float castCounter;

    private Camera cam;
    private Vector2 moveInput;

    public SpriteRenderer bodySR;

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLength = .5f, dashCooldown = 1f, dashInvis = .5f;
    [HideInInspector]
    public float dashCounter;
    private float dashCoolCounter;

    private float speedSmoothing = 0.1f; // Smoothing factor for speed
    private float smoothedSpeed; // Smoothed speed value

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        activeMoveSpeed = movementSpeed;

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
            if (animator == null)
            {
               ;
            }
        }
    }

    void Update()
    {
        // Handle Movement Input
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // Apply Movement
        rigidBody.velocity = moveInput * activeMoveSpeed;

        // Smooth the Speed value to avoid twitching
        float targetSpeed = moveInput.magnitude; // Calculate the magnitude of the movement
        smoothedSpeed = Mathf.Lerp(smoothedSpeed, targetSpeed, speedSmoothing);

        // Define an epsilon value to consider as "close to zero"
        const float epsilon = 0.001f;

        if (animator != null)
        {
            if (Mathf.Abs(smoothedSpeed) < epsilon) // Check if smoothedSpeed is close enough to zero
            {
                animator.Play("HeikoIdle"); // Make sure this matches your state name in the Animator
               
            }
            else
            {
                animator.SetFloat("Speed", smoothedSpeed);
               
            }
        }
        else
        {
           
        }

        // Make the Character Face the Movement Direction
        if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Firing Logic
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(firingObject, firePoint.position, firePoint.rotation);
            castCounter = timeBetweenCasts;
        }

        if (Input.GetMouseButton(0))
        {
            castCounter -= Time.deltaTime;

            if (castCounter < 0)
            {
                Quaternion disturbance = Quaternion.Euler(0, 0, Random.Range(0.1f, 1.1f) * 10f);
                Quaternion disturbedRotation = firePoint.rotation * disturbance;
                Instantiate(firingObject, firePoint.position, disturbedRotation);
                castCounter = timeBetweenCasts;
            }
        }

        // Handle Dashing Logic
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLength;
                if (animator != null)
                {
                    animator.SetTrigger("dash");
                }
                PlayerHealthController.instance.InvincibilityFrames();

            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {
                activeMoveSpeed = movementSpeed;
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }
}

