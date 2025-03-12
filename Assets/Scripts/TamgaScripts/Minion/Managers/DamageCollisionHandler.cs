using UnityEngine;

public class DamageCollisionHandler : ICollisionHandler
{
    private int damage;
    private Vector2 knockbackForce;

    public DamageCollisionHandler(int damage, Vector2 knockbackForce)
    {
        this.damage = damage;
        this.knockbackForce = knockbackForce;
    }

    public void HandleCollision(GameObject target)
    {
        var damageable = target.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }
    }
}
