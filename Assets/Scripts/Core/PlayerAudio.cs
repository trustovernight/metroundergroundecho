using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Footstep Settings")]
    public AudioClip[] footstepClips;   // Assign footstep sounds

    [Header("Landing Settings")]
    public AudioClip[] landingClips;    // Assign landing sounds

    private AudioSource footstepSource;
    private AudioSource landingSource;

    void Awake()
    {
        // Create two separate AudioSources
        footstepSource = gameObject.AddComponent<AudioSource>();
        landingSource = gameObject.AddComponent<AudioSource>();

        // Optional settings
        footstepSource.loop = false;

        landingSource.loop = false;
    }

    // Call this whenever the player steps
    public void PlayFootstep()
    {
        if (footstepClips.Length == 0) return;

        AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
        footstepSource.PlayOneShot(clip, 1f);
    }

    // Call this whenever the player lands
    public void PlayLanding()
    {
        if (landingClips.Length == 0) return;

        AudioClip clip = landingClips[Random.Range(0, landingClips.Length)];
        landingSource.PlayOneShot(landingClips[0], 1f);
    }
}