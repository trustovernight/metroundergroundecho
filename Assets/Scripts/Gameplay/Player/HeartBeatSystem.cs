using UnityEngine;

public class HeartBeatSystem : MonoBehaviour
{
    public Transform monster;       
    public AudioSource heartAudio;  
    public GameObject player;

    public float maxDistance;  
    public float minPitch;    
    public float maxPitch;      

    void FixedUpdate() 
    {
        float distance = Vector3.Distance(player.transform.position, monster.position);

        if (distance <= maxDistance)
        {
            float t = 1 - (distance / maxDistance);
            heartAudio.pitch = Mathf.Lerp(minPitch, maxPitch, t);

            if (!heartAudio.isPlaying)
                heartAudio.Play();
        } else heartAudio.Stop();
    }
}