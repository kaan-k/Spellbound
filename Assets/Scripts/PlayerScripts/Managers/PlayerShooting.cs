using UnityEngine;

public class PlayerShooting : MonoBehaviour, IPlayerShooting
{
    [Header("Shooting Settings")]
    public GameObject[] unlockedTamgas;
    public GameObject firingObject;
    public Transform firePoint;
    public float timeBetweenCasts;
    private int x = 0;
    private float castCounter;

    public AudioClip shootSound;
    private AudioSource audioSource;

    public CameraShake cameraShake;

    private void Start()
    {
        firingObject = unlockedTamgas[x];
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        UpdateShooting();
        ToggleTamgas();
    }


    public void ToggleTamgas()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            x++;
            if (x >= unlockedTamgas.Length)
            {
                x = 0;
            }
            firingObject = unlockedTamgas[x];
        }
    }


    public void UpdateShooting()
    {

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(firingObject, firePoint.position, firePoint.rotation);
            audioSource.PlayOneShot(shootSound);
            StartCoroutine(cameraShake.Shake(0.1f, 0.1f));
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
                audioSource.PlayOneShot(shootSound);
                StartCoroutine(cameraShake.Shake(0.1f, 0.1f));
                castCounter = timeBetweenCasts;
            }
        }
    }
}
