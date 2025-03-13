using UnityEngine;

public class PlayerShooting : MonoBehaviour, IPlayerShooting
{
    [Header("Shooting Settings")]
    public GameObject firingObject;
    public Transform firePoint;
    public float timeBetweenCasts;

    private float castCounter;

    private void Update()
    {
        UpdateShooting();
    }

    public void UpdateShooting()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(firingObject, firePoint.position, Quaternion.identity);
            castCounter = timeBetweenCasts;
        }


        if (Input.GetMouseButton(0))
        {
            castCounter -= Time.deltaTime;
            if (castCounter < 0)
            {

                Quaternion disturbance = Quaternion.Euler(0, 0, Random.Range(0.1f, 1.1f) * 10f);
                Quaternion disturbedRotation = firePoint.rotation * disturbance;
                Instantiate(firingObject, firePoint.position, disturbedRotation);
                castCounter = timeBetweenCasts;
            }
        }
    }
}
