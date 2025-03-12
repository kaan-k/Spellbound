using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMinion : MonoBehaviour
{
    public int damage;
    private MoveTowards moveTowards;
    private TargetSelector targetSelector;
    private CollisionHandler collisionHandler;
    private void Awake()
    {
        collisionHandler = GetComponent<CollisionHandler>();
        moveTowards = GetComponent<MoveTowards>();
        targetSelector = GetComponent<TargetSelector>();
    }
    void Update()
    {
        GameObject target = targetSelector.SelectTarget(targetSelector.targetTag);
        moveTowards.Move(target);
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
