using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffController : MonoBehaviour
{
    public float rotationSpeed = 5.0f; // Speed at which the staff rotates
    public float timeBetweenFiring = 0.5f; // Time between each fire
    public GameObject bullet; // Reference to the projectile or spell prefab
    public Transform bulletTransform; // Transform where the projectile should be instantiated (spawn point)

    private Camera mainCam; // Reference to the main camera
    private bool canFire = true; // Flag to check if the staff can fire
    private float timer; // Timer for controlling the firing rate

    private Transform playerTransform; // Reference to the player's transform
    private Vector3 initialScale; // Original scale of the staff

    void Start()
    {
        // Get the main camera reference
        mainCam = Camera.main;

        // Get the reference to the player (assuming the staff is a child of the player)
        playerTransform = transform.parent;

        // Store the original scale of the staff
        initialScale = transform.localScale;
    }

    void Update()
    {
        // Check if the main camera is null
        if (mainCam == null)
        {
            Debug.LogError("Main Camera not found! Make sure your scene has a Camera tagged as 'MainCamera'.");
            return; // Exit the Update function if the camera is missing
        }

        // Get mouse position in world space
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Keep the staff on the same plane as the player

        // Calculate the direction to point the staff
        Vector3 direction = mousePos - transform.position;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Determine if the player is facing left or right
        if (playerTransform != null && playerTransform.localScale.x < 0)
        {
            // Player is facing left; flip the staff horizontally
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
            // Adjust the rotation to face the mouse correctly
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rotZ), rotationSpeed * Time.deltaTime);
        }
        else
        {
            // Player is facing right; use the normal scale
            transform.localScale = initialScale;
            // Rotate the staff towards the mouse smoothly
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rotZ), rotationSpeed * Time.deltaTime);
        }

        // Firing logic
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        // Check for mouse input to fire
        if (Input.GetMouseButton(0) && canFire)
        {
            Fire();
        }
    }

    void Fire()
    {
        // Check if the bullet prefab or bulletTransform is missing
        if (bullet == null)
        {
            Debug.LogError("Bullet prefab is not assigned in the Inspector!");
            return; // Exit the Fire function if bullet is missing
        }

        if (bulletTransform == null)
        {
            Debug.LogError("Bullet Transform is not assigned in the Inspector!");
            return; // Exit the Fire function if bulletTransform is missing
        }

        canFire = false;
        Instantiate(bullet, bulletTransform.position, bulletTransform.rotation); // Instantiate the bullet or spell at the spawn point
    }
}
