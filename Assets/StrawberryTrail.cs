using UnityEngine;

public class StrawberryTrail : MonoBehaviour
{
    [Header("Trail Settings")]
    public GameObject splatterPrefab;
    public float dropInterval = 0.4f;
    public float splatterLifetime = 1.5f;
    public float minMoveThreshold = 0.01f;

    private float dropTimer;
    private Vector3 lastPosition;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        float movedDistance = Vector3.Distance(transform.position, lastPosition);
        dropTimer -= Time.deltaTime;

        if (movedDistance > minMoveThreshold && dropTimer <= 0f)
        {
            DropSplatter();
            dropTimer = dropInterval;
            lastPosition = transform.position;
        }
    }

    void DropSplatter()
    {
        if (splatterPrefab != null)
        {
            GameObject splatter = Instantiate(splatterPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            splatter.transform.localScale *= Random.Range(0.9f, 1.2f);
            Destroy(splatter, splatterLifetime);
        }
    }
}
