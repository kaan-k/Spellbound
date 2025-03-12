using UnityEngine;

public class FireMinion : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private string enemyTag = "Enemy";
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private Vector2 knockbackForce = Vector2.right;
    [SerializeField] public bool hasTarget = false;


    private ITargetSelector targetSelector;
    private IMover mover;
    private ICollisionHandler collisionHandler;

    private GameObject currentTarget;

    private void Awake()
    {

        targetSelector = new EnemyTargetSelector(enemyTag);
        mover = new MinionMover(rigidBody, movementSpeed);
        collisionHandler = new DamageCollisionHandler(damageAmount, knockbackForce);
    }

    private void Start()
    {
        // Initialize rotation (if needed) and acquire the first target.
        transform.rotation = Quaternion.identity;
        currentTarget = targetSelector.SelectTarget();
    }

    private void Update()
    {
        if (currentTarget != null)
        {
            mover.MoveTowards(currentTarget.transform.position, transform.position);
            hasTarget = true;
        }
        else
        {

            // Attempt to re-acquire a target if lost.
            currentTarget = targetSelector.SelectTarget();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(enemyTag))
        {
            collisionHandler.HandleCollision(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
