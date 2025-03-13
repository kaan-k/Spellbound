using UnityEngine;

public class FireMinion : MonoBehaviour
{
    public int damage;
    public bool hasTarget;
    private ITargetSelector targetSelector;
    private IMovable movement;
    private CollisionHandler collisionHandler;
    private IDistanceCalculator distanceCalculator;
    private IFlocker flocker;
    public Rigidbody2D rb;
    private void Awake()
    {
        collisionHandler = GetComponent<CollisionHandler>();
        targetSelector = GetComponent<ITargetSelector>();
        movement = GetComponent<IMovable>();
        distanceCalculator = GetComponent<IDistanceCalculator>();
        flocker = GetComponent<IFlocker>();
        //rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        GameObject target = targetSelector.SelectTarget("Enemy");

        if (target == null)
        {
           
            target = targetSelector.SelectTarget("Player");
            float distanceToPlayer = distanceCalculator.CalculateDistance(this.gameObject.transform, target.transform);
            hasTarget = true;
            flocker.Flock(target.transform, rb);

        }
        else
        {
            movement.Move(target);
            hasTarget = true;
        }


        hasTarget = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Damageable>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
