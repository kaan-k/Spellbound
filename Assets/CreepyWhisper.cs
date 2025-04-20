using UnityEngine;

public class CreepyWhisper : MonoBehaviour
{
    public AudioClip whisperClip;
    public float minDelay = 5f;
    public float maxDelay = 15f;

    private AudioSource audioSource;
    private float nextWhisperTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ScheduleNextWhisper();
    }

    void Update()
    {
        if (Time.time >= nextWhisperTime)
        {
            if (whisperClip != null && audioSource != null && !audioSource.isPlaying)
            {
                audioSource.pitch = Random.Range(0.95f, 1.05f);
                audioSource.PlayOneShot(whisperClip);
            }
            ScheduleNextWhisper();
        }
    }

    void ScheduleNextWhisper()
    {
        nextWhisperTime = Time.time + Random.Range(minDelay, maxDelay);
    }
}
