using UnityEngine;

public class PlayerAnimation : MonoBehaviour, IPlayerAnimation
{
    [Header("Animation Settings")]
    public Animator animator;
    public PlayerMovement playerMovement;

    private const float epsilon = 0.001f;

    private void Update()
    {
        UpdateAnimation();
    }

    public void UpdateAnimation()
    {
        if (animator != null && playerMovement != null)
        {
            float speed = playerMovement.CurrentSpeed;
            if (Mathf.Abs(speed) < epsilon)
            {
                animator.Play("HeikoIdle"); 
            }
            else
            {
                animator.SetFloat("Speed", speed);
            }
        }
    }
}
