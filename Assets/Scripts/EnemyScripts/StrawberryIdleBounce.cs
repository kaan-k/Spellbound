using UnityEngine;

public class StrawberryIdleBounce : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float bounceSpeedMin = 1.2f;
    public float bounceSpeedMax = 2.0f;

    public float bounceHeightMin = 0.02f;
    public float bounceHeightMax = 0.05f;

    [Header("Squash & Stretch")]
    public float squashFactor = 0.1f;

    private float bounceSpeed;
    private float bounceHeight;
    private Vector3 originalLocalPos;
    private Vector3 originalScale;

    void Start()
    {
        bounceSpeed = Random.Range(bounceSpeedMin, bounceSpeedMax);
        bounceHeight = Random.Range(bounceHeightMin, bounceHeightMax);
        originalLocalPos = transform.localPosition;
        originalScale = transform.localScale;
    }

    void Update()
    {
        float time = Time.time * bounceSpeed;
        float bounce = Mathf.Sin(time) * bounceHeight;

        // Only modify localPosition (not global position)
        transform.localPosition = originalLocalPos + new Vector3(0, bounce, 0);

        // Apply squash/stretch
        float stretch = 1 + (Mathf.Sin(time) * squashFactor);
        float squash = 1 - (Mathf.Sin(time) * squashFactor);

        transform.localScale = new Vector3(originalScale.x * squash, originalScale.y * stretch, originalScale.z);
    }
}
