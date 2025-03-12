using UnityEngine;

public class PlayerDash : MonoBehaviour, IPlayerDash
{
    [Header("Dash Settings")]
    public float dashLength = 0.5f;
    public float dashCooldown = 1f;
    public float dashSpeed = 8f;
    public float dashInvis = 0.5f; 

    public float dashCounter;
    private float dashCoolCounter;

    // References to other components.
    public PlayerMovement playerMovement;
    public Animator animator; 

    private void Update()
    {
        UpdateDash();
    }

    public void UpdateDash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (dashCoolCounter <= 0 && dashCounter <= 0)
            {

                playerMovement.SetMoveSpeed(dashSpeed);
                dashCounter = dashLength;

                if (animator != null)
                {
                    animator.SetTrigger("dash");
                }


                if (PlayerHealthController.instance != null)
                {
                    PlayerHealthController.instance.InvincibilityFrames();
                }
            }
        }

        if (dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if (dashCounter <= 0)
            {

                playerMovement.SetMoveSpeed(playerMovement.movementSpeed);
                dashCoolCounter = dashCooldown;
            }
        }

        if (dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }
}
