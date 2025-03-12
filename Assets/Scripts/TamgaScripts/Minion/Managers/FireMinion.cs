using UnityEngine;

public class FireMinion : MonoBehaviour
{
    public int damage;
    private ITargetSelector targetSelector;
    private IMovable movement;
    private CollisionHandler collisionHandler;

    private void Awake()
    {
        collisionHandler = GetComponent<CollisionHandler>();
        targetSelector = GetComponent<ITargetSelector>();
        movement = GetComponent<IMovable>();
    }

    private void Update()
    {
        GameObject target = targetSelector.SelectTarget("Enemy");
        if (target == null)
        {
            target = targetSelector.SelectTarget("Player");
        }

        movement.Move(target);
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
