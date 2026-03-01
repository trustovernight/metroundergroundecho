using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioSource landingSource;

    // Call this whenever the player steps
    public void PlayFootstep()
    {
        footstepSource.Play();
    }

    // Call this whenever the player lands
    public void PlayLanding()
    {
        landingSource.Play();
    }
}