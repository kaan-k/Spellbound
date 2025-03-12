using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public Rigidbody2D rigidBody;
    public SpriteRenderer bodySR;
    public Animator animator;
    public PlayerMovement playerMovement;
    public PlayerShooting playerShooting;
    public PlayerDash playerDash;
    public PlayerAnimation playerAnimation;

    private void Awake()
    {
        instance = this;
    }
}
