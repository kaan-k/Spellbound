using UnityEngine;

public class EnemyShooting : MonoBehaviour, IAttackable
{
    public bool shouldShoot;
    public GameObject projectile;
    public Transform firePoint;
    public float fireRate;
    public float shootingRange;
    private float fireCounter;

    private void Update()
    {
        Attack();
    }

    private bool IsPlayerInRange()
    {
        return Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootingRange;
    }

    public void Attack()
    {
        if (shouldShoot && IsPlayerInRange())
        {
            fireCounter -= Time.deltaTime;
            if (fireCounter <= 0)
            {
                fireCounter = fireRate;
                Instantiate(projectile, firePoint.position, firePoint.rotation);
            }
        }
    }
}
