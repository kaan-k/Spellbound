using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.WebCam;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float movementSpeed;

    public Transform staffArm;

    public Rigidbody2D rigidBody;

    public Animator animator;

    public GameObject firingObject;
    public Transform firePoint;

    public float timeBetweenCasts;
    float castCounter;

    private Camera cam;
    private Vector2 moveInput;

    public SpriteRenderer bodySR;

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLenght = .5f, dashCooldown = 1f, dashInvis = .5f;
    [HideInInspector]
    public float dashCounter;
    private float dashCoolCounter;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
 
        cam = Camera.main;
        activeMoveSpeed = movementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
        rigidBody.velocity = moveInput * activeMoveSpeed;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.localPosition);


        if (mousePosition.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            staffArm.localScale = new Vector3(-1, -1, 1);
        }
        else
        {
            transform.localScale = Vector3.one;
            staffArm.localScale = Vector3.one;
        }

        Vector2 offset = new Vector2(mousePosition.x - screenPoint.x, mousePosition.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        staffArm.rotation = Quaternion.Euler(0, 0, angle);
        //transform.position += new Vector3(moveInput.x * Time.deltaTime * movementSpeed,moveInput.y * Time.deltaTime * movementSpeed,0f);

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
                Quaternion disturbance = Quaternion.Euler(0, 0, Random.Range(0.1f,1.1f) * 10f);
                Quaternion disturbedRotation = firePoint.rotation * disturbance;
                Instantiate(firingObject, firePoint.position, disturbedRotation);
                castCounter = timeBetweenCasts;
            }



        }


        if (moveInput != Vector2.zero)
        {
            Debug.Log(moveInput);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {
                activeMoveSpeed = dashSpeed;
                dashCounter = dashLenght;
                animator.SetTrigger("dash");
                PlayerHealthController.instance.InvinciblityFrames();
            }

        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0)
            {
                activeMoveSpeed = movementSpeed;
                dashCoolCounter = dashCooldown;

            }
        }
        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }    
    }
}