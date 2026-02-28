using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Footstep Settings")]
    public AudioClip[] footstepClips;   // Assign footstep sounds
    [Range(0f, 1f)] public float footstepVolume = 0.5f;

    [Header("Landing Settings")]
    public AudioClip[] landingClips;    // Assign landing sounds
    [Range(0f, 1f)] public float landingVolume = 1f;

    private AudioSource footstepSource;
    private AudioSource landingSource;

    void Awake()
    {
        // Create two separate AudioSources
        footstepSource = gameObject.AddComponent<AudioSource>();
        landingSource = gameObject.AddComponent<AudioSource>();

        // Optional settings
        footstepSource.loop = false;
        footstepSource.volume = footstepVolume;

        landingSource.loop = false;
        landingSource.volume = landingVolume;
    }

    // Call this whenever the player steps
    public void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        footstepSource.PlayOneShot(clip, footstepVolume);
    }

    // Call this whenever the player lands
    public void PlayLanding()
    {
        if (landingClips.Length == 0) return;

        AudioClip clip = landingClips[Random.Range(0, landingClips.Length)];
        landingSource.PlayOneShot(clip, landingVolume);
    }
}